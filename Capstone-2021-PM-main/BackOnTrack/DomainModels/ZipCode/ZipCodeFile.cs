using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DomainModels
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/02/12
    /// 
    /// Storage Model for zip code objects.
    /// </summary>
    public class ZipCodeFile
    {

        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool isServicable { get; set; }

    }

}
