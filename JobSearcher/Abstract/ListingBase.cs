using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using JobSearcher.Models;

namespace JobSearcher.Abstract
{
    internal abstract class ListingBase : BindableBase, IEquatable<ListingBase>, IComparable<ListingBase>
    {
        #region Properties

        [Display(AutoGenerateField = false)]
        public EmailInfo ApplyEmail
        {
            get => Get<EmailInfo>();
            set => Set(value);
        }

        [DisplayName("City")]
        public string City
        {
            get => Get<string>();
            set => Set(value);
        }

        [DisplayName("Company")]
        public string Company
        {
            get => Get<string>();
            set => Set(value);
        }

        [Display(AutoGenerateField = false)]
        public string Description
        {
            get => Get<string>();
            set => Set(value);
        }

        [DisplayName("Employment Type")]
        public string EmploymentType
        {
            get => Get<string>();
            set => Set(value);
        }

        [DisplayName("Heading")]
        public string Heading
        {
            get => Get<string>();
            set => Set(value);
        }

        [DisplayName("Listed On")]
        public DateTime ListingTime
        {
            get => Get<DateTime>();
            set => Set(value);
        }

        [Display(AutoGenerateField = false)]
        public Uri ListingUrl
        {
            get => Get<Uri>();
            set => Set(value);
        }

        [DisplayName("Salary")]
        public string SalaryRange
        {
            get => Get<string>();
            set => Set(value);
        }

        [Display(AutoGenerateField = false)]
        public bool Selected
        {
            get => Get<bool>();
            set => Set(value);
        }

        #endregion

        #region Public Methods

        public int CompareTo(ListingBase other)
        {
            if (ListingTime != default(DateTime))
            {
                return ListingTime.CompareTo(other.ListingTime);
            }

            return -1;
        }

        public bool Equals(ListingBase other)
        {
            return other != null && (ApplyEmail?.Equals(other.ApplyEmail) ?? true) && (City?.Equals(other.City) ?? true) && (Company?.Equals(other.Company) ?? true) && (Description?.Equals(other.Description) ?? true) && (EmploymentType?.Equals(other.EmploymentType) ?? true) && (Heading?.Equals(other.Heading) ?? true) && (ListingUrl?.Equals(other.ListingUrl) ?? true) && (SalaryRange?.Equals(other.SalaryRange) ?? true);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ListingBase other))
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
                hash = (hash * 16777619) ^ (ApplyEmail?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (City?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (Company?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (Description?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (EmploymentType?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (Heading?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (ListingUrl?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (SalaryRange?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public virtual void ParseInitialNode(HtmlNode htmlNode) => throw new NotImplementedException(nameof(ParseInitialNode));

        public virtual void ParseRemainingData(HtmlNode htmlNode) => throw new NotImplementedException(nameof(ParseRemainingData));

        #endregion

        #region Protected Methods

        protected string CleanString(string s)
        {
            return new string(HttpUtility.HtmlDecode(s)?.Trim('\n', '\r', '\t', ' ', '-', '<', '>').Replace("\n\n", "\n").Where(c => c <= sbyte.MaxValue).ToArray());
        }

        #endregion
    }
}