using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF;
using VDS.RDF.Query.Datasets;
using CPSV_API.Models;

namespace CPSV_API.Repositories
{
    public static class PublicOrganisationData
    {
        
        public static  SparqlResultSet fetchData(SparqlParameterizedString queryString)
        {
            Options.HttpDebugging = true;

            //queryString = new SparqlParameterizedString();

            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://data.dai.uom.gr:8890/sparql"), "http://data.dai.uom.gr:8890/CPSV-AP");

            queryString.Namespaces.AddNamespace("cpsv", new Uri("http://purl.org/vocab/cpsv#"));
            queryString.Namespaces.AddNamespace("dcterms", new Uri("http://purl.org/dc/terms/"));
            queryString.Namespaces.AddNamespace("dct", new Uri("http://purl.org/dc/terms/"));
            queryString.Namespaces.AddNamespace("cv", new Uri("http://data.europa.eu/m8g/"));

            Console.WriteLine(queryString.ToString());
            RemoteQueryProcessor processor = new RemoteQueryProcessor(endpoint);

            SparqlQueryParser parser = new SparqlQueryParser();
            SparqlQuery query = parser.ParseFromString(queryString);
            SparqlResultSet results = (SparqlResultSet)query.Process(processor);

            return results;


        }
        
    }
}
