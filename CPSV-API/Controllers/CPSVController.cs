using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF;
using VDS.RDF.Query.Datasets;
using CPSV_API.Models;
using CPSV_API.Repositories;
using Newtonsoft.Json;

namespace CPSV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CPSVController : ControllerBase
    {
        
        // GET: api/allPublicServices : returns all URI's of UoM Public Services 
        [HttpGet("allPublicServices")]
        public ActionResult<PublicService> GetPublicServices()
        {

            SparqlParameterizedString queryString = new SparqlParameterizedString();
            //SPARQ Command that retrieved the specified data
            queryString.CommandText = "SELECT DISTINCT ?PS_uri ?name {?PS_uri a cpsv:PublicService. ?PS_uri dct:title ?name.}";
            SparqlResultSet results = PublicOrganisationData.fetchData(queryString);
            List<PublicService> publicServices = new List<PublicService>();

            foreach (var result in results)
            {

                PublicService ps = new PublicService();
                ps.createPublicService(result);
               
                publicServices.Add(ps);

            }
            return Ok(publicServices);

        }

        //GET api/PublicService(id) : returns all the information about this specific PS
        [HttpGet("PublicService/{id}")]
        public ActionResult<List<string>> GetPublicService(string id)
        {
            SparqlParameterizedString queryString = new SparqlParameterizedString();
            queryString.CommandText = "SELECT DISTINCT ?field ?description WHERE {<http://data.dai.uom.gr:8890/PublicServices/id/ps/" + id + "> ?field ?description}";
            SparqlResultSet results = PublicOrganisationData.fetchData(queryString);

            PublicService ps = new PublicService();
            
            ps.createPublicService(results, id);

            return Ok(ps);

        }

        ///GET api/PublicService(id) : returns all the information about this specific PS
        [HttpGet("allEvidences")]
        public ActionResult<Evidence> GetEvidences()
        {
            SparqlParameterizedString queryString = new SparqlParameterizedString();

            queryString.CommandText = "SELECT DISTINCT ?Evidence_name ?URI {?URI a cv:Evidence. ?URI dct:title  ?Evidence_name.}";
            SparqlResultSet results = PublicOrganisationData.fetchData(queryString);

            List<Evidence> Evidences = new List<Evidence>();
            foreach (var result in results)
            {

                Evidence ev = new Evidence();
                ev.createEvidence(result);
                Evidences.Add(ev);

            }
            return Ok(Evidences);
        }

        ///GET api/PublicService(id) : returns all the information about this specific PS
        [HttpGet("allFormalFrameworks")]

        public ActionResult<Evidence> GetFormalFrameworks()
        {

            SparqlParameterizedString queryString = new SparqlParameterizedString();
            queryString.CommandText = "SELECT DISTINCT ?o ?name {?o a cpsv:FormalFramework. ?o dct:title ?name. }";
            SparqlResultSet results = PublicOrganisationData.fetchData(queryString);

            List<FormalFramework> formalFrameworks = new List<FormalFramework>();
            foreach (var result in results)
            {
                FormalFramework legalResource = new FormalFramework();
                legalResource.createFormalFramework(result);
                formalFrameworks.Add(legalResource);
            }
            

            return Ok(formalFrameworks);
        }


        [HttpGet("PublicServiceswithFormalFramework/{id}/{id2}")]

        public ActionResult<PublicService> PublicServiceswithFormalFrameworks(string id, string id2)
        {
            SparqlParameterizedString queryString = new SparqlParameterizedString();

            queryString.CommandText = "  SELECT DISTINCT ?PS_uri ?name WHERE {?PS_uri cv:hasFormalFramework <http://data.dai.uom.gr:8890/LegalFramework/id/"+id+"/"+ id2 + ">. ?PS_uri dct:title ?name.}";
            SparqlResultSet results = PublicOrganisationData.fetchData(queryString);
            List<PublicService> publicServices = new List<PublicService>();

            foreach (var result in results)
            {

                PublicService ps = new PublicService();
                ps.createPublicService(result);

                publicServices.Add(ps);

            }
            return Ok(publicServices);
        }

        //SELECT DISTINCT ?a ?b
        //WHERE {
        //?a cpsv:hasInput <http://data.dai.uom.gr:8890/PublicServices/id/doc/Certificate0021>.
        //?a dct:title ?b.
        //} */

        //select ?PSname ?cost_value  ?sector ?currency ?Processing_Time
        //where{
        //?x a cpsv:PublicService.
        //?x dct:title ?PSname.
        //?x cv:sector ?sector.
        //?x cv:hasCost ?cost.
        //?cost cv:value ?cost_value.
        //?cost cv:currency ?currency.
        //?x cv:processingTime ?Processing_Time.
        //}
        //ORDER BY(?PSname)*/




        //select(count(? ps) as ?Number_of_PS)
        //where {
        //?ps a cpsv:PublicService.
        //} */


        //SELECT DISTINCT ?a 
        //WHERE {
        //?a a cv:PublicOrganisation
        //} */ 

    }
}