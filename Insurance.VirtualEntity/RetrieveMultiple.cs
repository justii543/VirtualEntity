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
    public class RetrieveMultiple : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService Trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            Trace.Trace("service Established");

            if (context.MessageName == "RetrieveMultiple")
            {
                Trace.Trace("Execution Begins");
                EntityCollection entityCollection = new EntityCollection();
                try
                {
                    using (var connection =PolicyAzureDB.PolicyAzureDb())
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter("select * from [dbo].[Policy_master]", connection))
                        {
                            DataTable Table = new DataTable();
                            adapter.Fill(Table);
                            int i = 0;
                            foreach (DataRow row in Table.Rows)
                            {
                                Entity Policy = new Entity(PolicyMaster.ENTITYNAME)
                                {
                                    [PolicyMaster.Fields.NAME] = "Master" + i,
                                    [PolicyMaster.Fields.STARTDATE] = (int)row[1],
                                    [PolicyMaster.Fields.END_DATE] = (int)row[2],
                                    [PolicyMaster.Fields.INTEREST_PERCENT] = (int)row[3],
                                    [PolicyMaster.Fields.POLICYMASTERID] = new Guid(row[0].ToString())
                                    
                                };
                                var a = Policy.Attributes[PolicyMaster.Fields.STARTDATE];
                                i++;
                                entityCollection.Entities.Add(Policy);
                            }
                        }
                    }
                    context.OutputParameters["BusinessEntityCollection"] = entityCollection;
                }
                catch(Exception e)
                {
                    Trace.Trace(e.Message);
                }
            }
            else
            {
                Trace.Trace("Invalid Input");
            }
            
        }
    }
}
