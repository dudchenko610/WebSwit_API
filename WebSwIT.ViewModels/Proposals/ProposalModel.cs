﻿using System;

namespace WebSwIT.ViewModels.Proposals
{
    public class ProposalModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
