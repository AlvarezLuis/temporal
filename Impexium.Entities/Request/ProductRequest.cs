using Impexium.Entities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Impexium.Entities.Request
{
    public class ProductRequest
    {
        [JsonProperty("Id")]        
        public int? Id { get; set; }
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Amount")]
        public int Amount { get; set; }

        public Product ConvertToProduct()
        {
            return new Product()
            {
                Id = Id ?? default(int),
                Name = Name,
                Description = Description,
                Amount = Amount
            };
        }
    }
}
