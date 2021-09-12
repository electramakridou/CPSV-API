using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF;
using VDS.RDF.Query.Datasets;

namespace CPSV_API.Models
{
    public class FormalFramework
    {
        public string Uri { get; set; }
        public string Name { get; set; }
        public void createFormalFramework(SparqlResult result)
        { 
                string temp = result.ToString();
                temp = temp.Replace("?PS_uri =", "");
                temp = temp.Replace("?name =", "");
                string[] temp2 = temp.Split(",");
            //below depending if the item has a keyword or not the code assigns the relative information to each corresponding field
               if (temp2[0].Contains("/LegalFramework/id/")) { this.Uri = temp2[0]; this.Name = temp2[1]; } else { this.Uri = temp2[1]; this.Name = temp2[0]; }


        }
         
    }
}
