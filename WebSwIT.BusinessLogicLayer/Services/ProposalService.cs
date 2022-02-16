using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Providers;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Proposals;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.Shared.Options;
using WebSwIT.ViewModels.Proposals;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class ProposalService : IProposalService
    {
        private readonly IProposalRepository _proposalRepository;
        private readonly IMapper _mapper;
        private readonly EmailProvider _emailProvider;
        private readonly EmailConnectionOptions _emailConnection;

        public ProposalService(
            IProposalRepository proposalRepository, 
            IMapper mapper,
            EmailProvider emailProvider,
            IOptions<EmailConnectionOptions> emailOptions)
        {
            _proposalRepository = proposalRepository;
            _mapper = mapper;
            _emailProvider = emailProvider;
            _emailConnection = emailOptions.Value;
        }

        public async Task<ProposalModel> CreateAsync(CreateProposalModel model)
        {
            var proposal = _mapper.Map<Proposal>(model);
            await _proposalRepository.CreateAsync(proposal);
            var proposalModel = _mapper.Map<ProposalModel>(proposal);

            await _emailProvider.SendEmailAsync(
                _emailConnection.MailAddress, 
                $"{proposal.Name} {proposal.Email} {proposal.PhoneNumber}",
                proposal.Message
            );

            return proposalModel;
        }

        public async Task DeleteAsync(Guid id)
        {
            var proposal = await _proposalRepository.GetByIdAsync(id);

            if (proposal is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            await _proposalRepository.DeleteAsync(proposal);
        }

        public async Task<ProposalModel> GetByIdAsync(Guid id)
        {
            var proposal = await _proposalRepository.GetByIdAsync(id);

            if (proposal is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            return _mapper.Map<ProposalModel>(proposal);
        }

        public async Task<PagedResponseModel<ProposalModel>> GetFilteredAsync(ProposalFilterModel model, PaginationFilterModel pagination)
        {
            var proposals = _proposalRepository.GetFiltered(model);
            var proposalsCount = proposals.Count();

            proposals = proposals.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

            var result = _mapper.Map<IEnumerable<ProposalModel>>(await proposals.ToListAsync());

            var pagedResponse = new PagedResponseModel<ProposalModel>
            {
                Data = result,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalItems = proposalsCount
            };

            return pagedResponse;
        }
    }
}
