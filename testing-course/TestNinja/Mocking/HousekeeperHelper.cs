using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TestNinja.Mocking
{
    public static class HousekeeperHelper
    {
        public static bool SendStatementEmails(
            DateTime statementDate, 
            IHouseKeeperRepository repository = null, 
            IHouseKeeperFileManager fileManager = null,
            IEmailFileManager emailManager = null,
            IMessager messager = null)
        {
            if (repository is null) repository = new HousekeeperRepository();
            if (fileManager is null) fileManager = new HouseKeeperFileManager();
            if (emailManager is null) emailManager = new EmailFileManager();
            if (messager is null) messager = new XtraMessageBox();

            var housekeepers = repository.GetHousekeepers();

            foreach (var housekeeper in housekeepers)
            {
                if (string.IsNullOrWhiteSpace(housekeeper.Email))
                    continue;

                var statementFilename = fileManager.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

                if (string.IsNullOrWhiteSpace(statementFilename))
                    continue;

                var emailAddress = housekeeper.Email;
                var emailBody = housekeeper.StatementEmailBody;

                try
                {
                    emailManager.EmailFile(emailAddress, emailBody, statementFilename,
                        string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
                }
                catch (Exception e)
                {
                    XtraMessageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress),
                        MessageBoxButtons.OK);
                }
            }

            return true;
        }
    }

    public enum MessageBoxButtons
    {
        OK
    }

    public interface IMessager
    {
        void Show(string s, string housekeeperStatements, MessageBoxButtons ok);
    }

    public class XtraMessageBox : IMessager
    {
        public void Show(string s, string housekeeperStatements, MessageBoxButtons ok)
        {
        }
    }

    public class MainForm
    {
        public bool HousekeeperStatementsSending { get; set; }
    }

    public class DateForm
    {
        public DateForm(string statementDate, object endOfLastMonth)
        {
        }

        public DateTime Date { get; set; }

        public DialogResult ShowDialog()
        {
            return DialogResult.Abort;
        }
    }

    public enum DialogResult
    {
        Abort,
        OK
    }

    public class SystemSettingsHelper
    {
        public static string EmailSmtpHost { get; set; }
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailFromEmail { get; set; }
        public static string EmailFromName { get; set; }
    }

    public class Housekeeper
    {
        public string Email { get; set; }
        public int Oid { get; set; }
        public string FullName { get; set; }
        public string StatementEmailBody { get; set; }
    }

    public class HousekeeperStatementReport
    {
        public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
        {
        }

        public bool HasData { get; set; }

        public void CreateDocument()
        {
        }

        public void ExportToPdf(string filename)
        {
        }
    }
}