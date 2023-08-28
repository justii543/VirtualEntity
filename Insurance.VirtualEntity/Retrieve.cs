using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.VirtualEntity
{
    public class Retrieve : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService Trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            Trace.Trace("service established");

            if (context.MessageName == "Retrieve")
            {
                Trace.Trace("Execution Begins");
                Guid Id = context.PrimaryEntityId;
                using(var connection= PolicyAzureDB.PolicyAzureDb())
                {
                    using (SqlDataAdapter Adapter =new SqlDataAdapter("select * from [dbo].[Policy_master] where Id='" + Id + "'", connection))
                    {
                        DataTable Table = new DataTable();
                        Adapter.Fill(Table);
                        try
                        {
                            Entity Policy = new Entity(PolicyMaster.ENTITYNAME)
                            {
                                [PolicyMaster.Fields.NAME] = "Master",
                                [PolicyMaster.Fields.STARTDATE] = (int)Table.Rows[0][1],
                                [PolicyMaster.Fields.END_DATE] = (int)Table.Rows[0][2],
                                [PolicyMaster.Fields.INTEREST_PERCENT] = (int)Table.Rows[0][3],
                                [PolicyMaster.Fields.POLICYMASTERID] = new Guid(Table.Rows[0][0].ToString())
                            };
                            context.OutputParameters["BusinessEntity"] = Policy;
                        }
                        catch(Exception e)
                        {
                            Trace.Trace(e.Message);
                        }
                    }
                }
            }
            else
            {
                Trace.Trace("Invalid Input");
            }
        }
    }
}
