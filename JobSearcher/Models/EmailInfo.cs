using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using JobSearcher.Abstract;

namespace JobSearcher.Models
{
    internal class EmailInfo : BindableBase, IEquatable<EmailInfo>
    {
        #region Constructors

        public EmailInfo(string mailToLine)
        {
            Parse(mailToLine);
        }

        public EmailInfo()
        {
            Body = "https://denver.craigslist.org";
            FromAddress = new MailAddress("cturiano@gmail.com");
            ToAddress = new MailAddress("cturiano@gmail.com");
            Subject = "Big Money Employment Opportunity";
        }

        #endregion

        #region Properties

        [Display(AutoGenerateField = false)]
        public string Body
        {
            get => Get<string>();
            set => Set(value);
        }

        [Display(AutoGenerateField = true)]
        public MailAddress FromAddress
        {
            get => Get<MailAddress>();
            set => Set(value);
        }

        [Display(AutoGenerateField = false)]
        public string Subject
        {
            get => Get<string>();
            set => Set(value);
        }

        [Display(AutoGenerateField = true)]
        public MailAddress ToAddress
        {
            get => Get<MailAddress>();
            set => Set(value);
        }

        #endregion

        #region Public Methods

        public bool Equals(EmailInfo other)
        {
            return other != null && Body.Equals(other.Body) && Subject.Equals(other.Subject) && ToAddress.Equals(other.ToAddress);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EmailInfo other))
            {
                return false;
            }

            return ReferenceEquals(this, obj) || Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash = (hash * 16777619) ^ (Body?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (FromAddress?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (ToAddress?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (Subject?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public override string ToString()
        {
            return ToAddress.Address;
        }

        #endregion

        #region Private Methods

        private void Parse(string mailToLine)
        {
            mailToLine = mailToLine.Replace("mailto:", "").Replace("?subject=", "~").Replace("&amp;body=", "~");
            var parts = mailToLine.Split('~');
            ToAddress = new MailAddress(parts[0]);
            Subject = WebUtility.UrlDecode(parts[1].Replace("+", " "));
            Body = WebUtility.UrlDecode(parts[2]);
        }

        #endregion
    }
}