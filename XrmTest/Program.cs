using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace XrmTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //CrmServiceClient crmSvc = new CrmServiceClient("friyank@Fapple.onmicrosoft.com", ConvertToSecureString("dell@123"),"NorthAmerica","fapple",isOffice365:true);
            using (CrmServiceClient crmSvc = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CrmString"].ConnectionString))
            {
                if (crmSvc.IsReady)
                {
                    //Guid RecordID = CreateSimpleAccount(crmSvc);
                    //AttachNotesToRecordUsingCreateAnnotation(crmSvc, RecordID);
                    //CreateNewRecord(crmSvc);
                }
                else { Console.WriteLine("Error Occorded : {0}", crmSvc.LastCrmError); }
            }
            Console.ReadLine();
        }

        private static Guid CreateNewRecord(CrmServiceClient crmSvc)
        {
            Dictionary<string, CrmDataTypeWrapper> inData = new Dictionary<string, CrmDataTypeWrapper>();
            inData.Add("name", new CrmDataTypeWrapper("account using crmSvc.CreateNewRecord", CrmFieldType.String));
            Guid Recordid2 = crmSvc.CreateNewRecord("account", inData);
            Console.WriteLine("Create Record {0}", Recordid2);
            return Recordid2;
        }

        private static void AttachNotesToRecordUsingCreateAnnotation(CrmServiceClient crmSvc, Guid RecordID)
        {
            Dictionary<string, CrmDataTypeWrapper> inData = new Dictionary<string, CrmDataTypeWrapper>();
            inData.Add("subject", new CrmDataTypeWrapper("This is a NOTE from the API", CrmFieldType.String));
            inData.Add("notetext", new CrmDataTypeWrapper("This is text that will go in the body of the note", CrmFieldType.String));
            crmSvc.CreateAnnotation("account", RecordID, inData);
        }

        private static Guid CreateSimpleAccount(CrmServiceClient crmSvc)
        {
            Entity E = new Entity("account");
            E["name"] = "Broda";
            var result = crmSvc.Create(E);
            Console.WriteLine("Create Record {0}", result.ToString());
            return result;
        }

        private static SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("missing pwd");

            var securePassword = new SecureString();
            foreach (char c in password)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }

    }
}
