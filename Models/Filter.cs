using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebApp.Models
{
    public class Filter
    {
        public enum FilterEntity
        {
            orderId,
            name,
            email,
            createdOn,
            excpectedDate,
            comment,
            done
        }

        public enum FilterType
        {
            exactString,
            approximateString,
            daterange,
            exactDate,
            exist
        }

        public string filterName { get; }

        public FilterEntity filterEntity { get; }

        public FilterType filterType { get; }
        
        public string value { get; set; }

        public Filter(string filterName, FilterEntity filterEntity, FilterType filterType, string value = "")
        {
            this.filterName = filterName;
            this.filterEntity = filterEntity;
            this.filterType = filterType;
            this.value = value;
        }
        
    }

    public class Filters
    {
        public List<Filter> filters = new List<Filter>();
        
        public Filters()
        {
            filters.Add(
                new Filter(
                    "OrderID (exact)",
                    Filter.FilterEntity.orderId,
                    Filter.FilterType.exactString));
            
            filters.Add(
                new Filter(
                    "Name (approx)",
                    Filter.FilterEntity.name,
                    Filter.FilterType.approximateString));
            
            filters.Add(
                new Filter(
                    "Email (approx)",
                    Filter.FilterEntity.email,
                    Filter.FilterType.approximateString));
            
            filters.Add(
                new Filter(
                    "CreatedOn (exact)",
                    Filter.FilterEntity.createdOn,
                    Filter.FilterType.exactDate));
            
            filters.Add(
                new Filter(
                    "ExpectedDate (exact)",
                    Filter.FilterEntity.excpectedDate,
                    Filter.FilterType.exactDate));
            
            filters.Add(
                new Filter(
                    "Date (exact)",
                    Filter.FilterEntity.done,
                    Filter.FilterType.exactDate));
            
            filters.Add(
                new Filter(
                    "Done",
                    Filter.FilterEntity.done,
                    Filter.FilterType.exist));
        }
    }
}