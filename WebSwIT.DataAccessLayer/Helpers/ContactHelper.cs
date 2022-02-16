using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSwIT.DataAccessLayer.Helpers
{
    public static class ContactHelper
    {
        public static string GetContactCollectionName(Guid uId1, Guid uId2) 
        {
            string userId1 = uId1.ToString();
            string userId2 = uId2.ToString();

            for (int i = 0; i < userId1.Length; i++)
            {
                if (userId1[i] > userId2[i])
                {
                    return $"chat.{userId1}.{userId2}";
                }
                else if (userId1[i] < userId2[i])
                {
                    return $"chat.{userId2}.{userId1}";
                }
            }

            return $"chat.{userId1}.{userId2}"; // ids are equal - imposible!
        }
    }
}
