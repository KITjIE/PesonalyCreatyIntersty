using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DLGCY.FilesWatcher.Helper
{
    public static class ApiClient
    {
        static readonly HttpClient httpClient = new HttpClient() { Timeout = new TimeSpan(0, 3, 0) };

        /// <summary>
        /// post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">方法名</param>
        /// <param name="postData">post数据</param>
        /// <param name="reqEnum">请求的枚举</param>
        /// <returns></returns>
        public static T PostResponse<T>(string baseUrl, string method, string postData) where T : class, new()
        {
            string url = $"{baseUrl}{method}";
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            T result = default(T);
            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;
                try
                {
                    result = JsonConvert.DeserializeObject<T>(s);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("远程服务器错误");
            }

            return result;
        }
        public static async Task<T> PostResponseAsync<T>(string baseUrl, string method, string postData) where T : class, new()
        {
            HttpClient httpClient = new HttpClient() { Timeout = new TimeSpan(0, 3, 0) };
            string url = $"{baseUrl}{method}";
            if (url.StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            T result = default(T);
            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string t = await response.Content.ReadAsStringAsync();
                string s = t;
                try
                {
                    result = JsonConvert.DeserializeObject<T>(s);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("远程服务器错误");
            }

            return result;
        }



        /// <summary>
        /// API请求参数
        /// </summary>
        public class ApiRequest
        {
            /// <summary>
            /// API类型
            /// </summary>
            public string ApiType { get; set; }
            /// <summary>
            /// 参数
            /// </summary>
            public object Parameters { get; set; }
            /// <summary>
            /// 方法名
            /// </summary>
            public string Method { get; set; }
            /// <summary>
            /// 上下文
            /// </summary>
            public RequestContext Context { get; set; } = new RequestContext();
        }

        /// <summary>
        /// 上下文
        /// </summary>
        public class RequestContext
        {
            /// <summary>
            /// 令牌
            /// </summary>
            public string Ticket { get; set; }
            /// <summary>
            /// 组织ID
            /// </summary>
            public int InvOrgId { get; set; } = 1;
        }

        /// <summary>
        /// 登录参数
        /// </summary>
        public class LoginParameter
        {
            public string Value { get; set; }
        }

        /// <summary>
        /// API返回参数
        /// </summary>
        public class ApiResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public object Result { get; set; }
            public RequestContext Context { get; set; } = new RequestContext();
        }

        public class LoginResult
        {
            public decimal UserId { get; set; }
            public decimal EmployeeId { get; set; }
            public string UserCode { get; set; }
            public string UserName { get; set; }
            public int InvOrg { get; set; }
        }

        public class WipMoveData
        {
            public string EmployeeCode { get; set; }
            public string EquipCode { get; set; }
            public string Barcode { get; set; }
            public string StationCode { get; set; }
        }

        public class ApiParameter
        {
            public object Value { get; set; }
        }

        public class RequestMES
        {
            public string ApiType { get; set; }
            //public List<> UserName { get; set; }

            public List<Paras> Parameters { get; set; }


        }


        public class Paras
        {
            public object Value { get; set; }

        }


        public class EQP_EmployeeParas
        {
            public string EquipCode { get; set; }
            public string EmployeeCode { get; set; }


        } 

    }
}
