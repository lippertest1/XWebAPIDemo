using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using X_1_FirstWebAPI.Model;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace X_1_FirstWebAPI.Controller
{
    public class ArtController : ApiController
    {
        SqlConnection conn;
        public ArtController() {

            string strCon = @"
Data Source=rdsd953u8w80j3b14qv2public.sqlserver.rds.aliyuncs.com,3433;
Initial Catalog=vart_campaign_db;
user id=campaign_user;
password=voice_club;";
            conn = new SqlConnection(strCon);
            conn.Open();
        }
        // GET api/<controller>
        public Art Get()
        {
            var art = new Art
            {
                artId = "1",		//唯一标识
                artName = "none param",		//名字
                openId = "",		//微信openid
                picKey = "lipper.jpg",		//作品照片七牛key
                css = "",		//css
                borderId = 7,	//borderId
                signKey = "none param",	//签名照片七牛key
                uploadDate = "1438629864464", 	//上传时间
                score = 1888,  	//随机分数
                commentIdList = "2",	//随机评语
                dialogIdList = "5,2,6"	    //随机对话
            };

            return art;

        }

        // POST api/<controller>
        public Art Get(string artId)
        {
            string strSQL = string.Format(@"
SELECT [artId]
      ,[artName]
      ,[openId]
      ,[picKey]
      ,[css]
      ,[borderId]
      ,[signKey]
      ,[uploadDate]
      ,[score]
      ,[commentIdList]
      ,[dialogIdList]
  FROM [vart_campaign_db].[dbo].[ArtList]
  where artId = '{0}'
            ", artId);
            SqlDataAdapter command = new SqlDataAdapter(strSQL, conn);
            //SqlCommand command = new SqlCommand(strSQL, conn); 
            //SqlDataReader sqlread = command.ExecuteReader();

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            command.Fill(ds, "ArtList");
            dt = ds.Tables["ArtList"];

            //foreach(DataRow dr in dt.Rows)        
            //{        
            //     object value = dr["ColumnsName"];        
            //}

            var art = new Art();

            if (dt.Rows.Count > 0)
            {
                art = new Art
                {
                    artId = dt.Rows[0]["artId"].ToString(),		//唯一标识
                    artName = dt.Rows[0]["artName"].ToString(),		//名字
                    openId = dt.Rows[0]["openId"].ToString(),		//微信openid
                    picKey = dt.Rows[0]["picKey"].ToString(),		//作品照片七牛key
                    css = dt.Rows[0]["css"].ToString(),		//css
                    borderId = Int32.Parse(dt.Rows[0]["borderId"].ToString()),	//borderId
                    signKey = dt.Rows[0]["signKey"].ToString(),	//签名照片七牛key
                    uploadDate = dt.Rows[0]["uploadDate"].ToString(), 	//上传时间
                    score = Int32.Parse(dt.Rows[0]["score"].ToString()),  	//随机分数
                    commentIdList = dt.Rows[0]["commentIdList"].ToString(),	//随机评语
                    dialogIdList = dt.Rows[0]["dialogIdList"].ToString()	    //随机对话
                };
            }
            else {
                art = new Art
                {
                    artId = "0",		//唯一标识
                    artName = "0",		//名字
                    openId = "0",		//微信openid
                    picKey = "0",		//作品照片七牛key
                    css = "0",		//css
                    borderId = 0,	//borderId
                    signKey = "0",	//签名照片七牛key
                    uploadDate = "0", 	//上传时间
                    score = 0,  	//随机分数
                    commentIdList = "0",	//随机评语
                    dialogIdList = "0"	    //随机对话
                };
            }

            return art;
        }
        [HttpPost]
        // POST: /Person/Delete/5 
        public void Post([FromBody]string value)//(Person obj )
        {
            Art art = (Art)JsonConvert.DeserializeObject(value, typeof(Art));

            Random ran = new Random();
            art.commentIdList = ran.Next(0, 19).ToString();
            art.score = ran.Next(8000, 9999);

            string strSQL = string.Format(@"
INSERT INTO [vart_campaign_db].[dbo].[ArtList]
    ([artId]
    ,[artName]
    ,[openId]
    ,[picKey]
    ,[css]
    ,[borderId]
    ,[signKey]
    ,[uploadDate]
    ,[score]
    ,[commentIdList]
    ,[dialogIdList])
VALUES
    ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}
", art.artId, art.artName, art.openId, art.picKey, art.css,
 art.borderId, art.signKey, art.uploadDate, art.score, art.commentIdList, art.dialogIdList);
            SqlDataAdapter command = new SqlDataAdapter(strSQL, conn);

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