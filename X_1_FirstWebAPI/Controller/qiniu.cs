using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Qiniu;
using Newtonsoft.Json;
using System.Dynamic;
using X_1_FirstWebAPI.Model;


namespace X_1_FirstWebAPI.Controller
{
    public class qiniuController : ApiController
    {
        // GET api/<controller>
        //ublic ienumerable<string> get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public QiniuResp Get()
        {
            Qiniu.Conf.Config.ACCESS_KEY = "TCZ4q7OROJf39XpZLh1ogRJjz63d7RPHGVe9S4FN";
            Qiniu.Conf.Config.SECRET_KEY = "pUjgTevL6cigNnIEQXho_3PD1POgixEQA9D8GWbZ";
            var policy = new Qiniu.RS.PutPolicy("vart-campaign", 3600);
            string upToken = policy.Token();
            //dynamic resp = new ExpandoObject();
            //resp.upToken = upToken;
            //return resp.ToStirng();

            QiniuResp resp = new QiniuResp();
            resp.uptoken = upToken;

            return resp;


        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}