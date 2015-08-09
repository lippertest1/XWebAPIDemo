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
using System.Configuration;

namespace X_1_FirstWebAPI.Controller
{
    public class ArtController : ApiController
    {
        SqlConnection conn;
        public ArtController() {

            string strCon = ConfigurationManager.ConnectionStrings["LogDBConn"].ToString();;
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
                score = "1888",  	//随机分数
                scoreComment = "test",  	//随机分数
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
      ,[scoreComment]
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
                    score = dt.Rows[0]["score"].ToString(),  	//随机分数
                    scoreComment = dt.Rows[0]["scoreComment"].ToString(),  	//随机分数
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
                    score = "0",  	//随机分数
                    commentIdList = "0",	//随机评语
                    dialogIdList = "0"	    //随机对话
                };
            }

            return art;
        }
        [HttpPost]
        // POST: /Person/Delete/5 
        public Art Post([FromBody]string value)//(Person obj )
        {
            Art art = (Art)JsonConvert.DeserializeObject(value, typeof(Art));

            //Random ran = new Random();
            //art.commentIdList = ran.Next(0, 19).ToString();
            //art.score = ran.Next(8000, 9999);

            //List<SpecialScore> SpecialScoreMap = new List<SpecialScore>();

            //格式必须是min|max|desc  从上往下匹配，不能有重复
            string[] NmMap = new string[] { 
                "1|9|吓死宝宝了", 
                "10|499|我感到了源自于你的深深恶意", 
                "500|999|拍照技术哪家强，中国山东找…蓝翔也救不了你啦~", 
                "1000|4999|经砖家鉴定，您的作品已荣获全国儿童艺术大赛入围奖", 
                "5000|8999|我想尝尝创作这幅作品的你的嘴角，是什么味道~", 
                "9000|9999|骚年,拯救中华艺坛的重任就交给你啦！", 
                "5000|8999|我想尝尝创作这幅作品的你的嘴角，是什么味道~", 
                "10000|29999|湿主，此画当真？",
                "30000|49999|这幅作品的颜值快要爆表啦！",
                "50000|69999|霸道总裁会爱上你~的作品！",
                "70000|89999|看了你的画，忘了我的TA，白天么么哒，晚上啪啪啪~",
                "90000|99999|如此稀世珍品，我一定要交给国家！"};

            //格式必须是tag|score|desc  (score不一定能直接用为score
            string[] SpMap = new string[] { 
                "负数|-ran|将功成万骨枯，一代渣作终入土…", 
                "无法估值(差)|无法估值|我从未见过如此厚颜无耻之图！", 
                "0|0|色即是空，空即是色，算你狠！", 
                "233|233|233？哼！好大的口气！", 
                "666|666|照片还是你拍的溜啊", 
                "100000|100000|不求最好，但求最贵，您的画作已被VART评选为年度最佳作品之一！", 
                "无法估值（好）|无法估值|此画只应天上有，人间难得几回闻！",
                "110|110|关于这幅画，我已经报警了…" };
            string[] SpMap233 = new string[] { "233", "2333", "23333" };
            string[] SpMap666 = new string[] { "666", "6666", "66666" };
            string SpMapScale = "";
            
            int red = 1; int black = 9;

            Random ran = new Random();
            //这里随机数是下整 [)
            if (ran.Next(red + black) < red)
            {
                SpMapScale = SpMap[ran.Next(SpMap.Length)];
                if (SpMapScale.Split('|').Length == 3)
                {
                    switch (SpMapScale.Split('|')[1])
                    {
                        case "-ran":
                            art.score = ran.Next(-10000, -1).ToString();
                            break;
                        case "233":
                            art.score = SpMap233[ran.Next(3)];
                            break;
                        case "666":
                            art.score = SpMap666[ran.Next(3)];
                            break;
                        default:
                            art.score = SpMapScale.Split('|')[1];
                            break;
                    }
                    art.scoreComment = SpMapScale.Split('|')[2];
                }
            }
            else {
                int intscore = ran.Next(99999);
                art.score = intscore.ToString();
                for (int i = 0; i < NmMap.Length; i++)
                {
                    if(NmMap[i].Split('|').Length==3){
                        if (intscore <= Int32.Parse(NmMap[i].Split('|')[1]) && intscore >= Int32.Parse(NmMap[i].Split('|')[0]))
                        {
                            art.scoreComment = SpMapScale.Split('|')[2];
                        }
                    }
                }
            }




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
    ,[scoreComment]
    ,[commentIdList]
    ,[dialogIdList])
VALUES
    ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11})
", art.artId, art.artName, art.openId, art.picKey, art.css,
 art.borderId, art.signKey, art.uploadDate, art.score, art.scoreComment, art.commentIdList, art.dialogIdList);
            SqlCommand command = new SqlCommand(strSQL, conn);
            //command.Connection.Open();
            int count = command.ExecuteNonQuery();
            return art;
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