using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.VirtualEntity
{
    public class PolicyMaster
    {
        public const string ENTITYNAME = "jus_policymaster";
        public class Fields
        {
            public const string NAME = "jus_name";
            public const string STARTDATE = "jus_startdate";
            public const string END_DATE = "jus_enddate";
            public const string INTEREST_PERCENT = "jus_interestpercentage";
            public const string POLICYMASTERID = "jus_policymasterid";
        }
    }
}
