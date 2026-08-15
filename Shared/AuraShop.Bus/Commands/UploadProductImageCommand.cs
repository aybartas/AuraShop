using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraShop.Bus.Commands
{
    public record UploadProductImageCommand
    {
        public Guid ProductId { get; set; }
        public string Filename { get; set; }
        public byte[] ImageData { get; set; }   
    }
}
