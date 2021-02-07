using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;

namespace dt_console
{
    class Program
    {
        String url = "";
        String name = "";

        public string Url { get => url; set => url = value; }

        static void Main(string[] args)
        {


            Program p = new Program();
           

            String data = "";
            String date = "";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "DAILY CHECK";

            while (p.Url == "")
            {
                Console.Write("Server:");     //

                p.Url = Console.ReadLine();
            }
            while (p.name == "")
            {
                Console.Write("ID:");
                p.name = Console.ReadLine();
            }

           p.list(p.name);

            while (true)
            {
                
                Console.Write(p.name + ">");
                String input = Console.ReadLine();
                
                switch (input.Split(' ')[0])
                {        
                    case "clear": Console.Clear(); break;
                    case "show":
                        {
                            date = input.Split(' ')[1]; if (date.Length == 8)
                            {
                                String temp = "";
                                temp += date[0]; temp += date[1];temp += date[2];temp += date[3];
                                temp += ".";
                                temp += date[4]; temp += date[5];
                                temp += ".";
                                temp += date[6]; temp += date[7];

                                date = temp;
                            }
                            p.show(date); break;
                        }
                    case "show-all":
                        {
                            p.show();
                            break;
                        }
                    case "post":
                        {
                            date = input.Split(' ')[1]; if (date.Length == 8)
                            {
                                String temp = "";
                                temp += date[0];
                                temp += date[1];
                                temp += date[2];
                                temp += date[3];
                                temp += ".";
                                temp += date[4];
                                temp += date[5];
                                temp += ".";
                                temp += date[6];
                                temp += date[7];

                                date = temp;
                            }

                            while (data!="quit")
                            {
                                data = Console.ReadLine();
                                if (data == "quit") break;
                                p.post(date, data);
                                
                            }
                            break;
                        }
                    case "delete":
                        {
                            if(input.Split(' ').Length == 3)
                            {
                                date = input.Split(' ')[1]; if (date.Length == 8)
                                {
                                    String temp = "";
                                    temp += date[0];
                                    temp += date[1];
                                    temp += date[2];
                                    temp += date[3];
                                    temp += ".";
                                    temp += date[4];
                                    temp += date[5];
                                    temp += ".";
                                    temp += date[6];
                                    temp += date[7];

                                    date = temp;
                                }
                                int index = int.Parse(input.Split(' ')[2]);
                                p.delete(date, index);
                            }
                            else if(input.Split(' ').Length == 2)
                            {
                                date = input.Split(' ')[1]; if (date.Length == 8)
                                {
                                    String temp = "";
                                    temp += date[0];
                                    temp += date[1];
                                    temp += date[2];
                                    temp += date[3];
                                    temp += ".";
                                    temp += date[4];
                                    temp += date[5];
                                    temp += ".";
                                    temp += date[6];
                                    temp += date[7];

                                    date = temp;
                                }
                                p.delete(date);
                            }
                            
                            break;
                        }
                    case "delete-all":
                        {
                            date = input.Split(' ')[1]; if (date.Length == 8)
                            {
                                String temp = "";
                                temp += date[0];
                                temp += date[1];
                                temp += date[2];
                                temp += date[3];
                                temp += ".";
                                temp += date[4];
                                temp += date[5];
                                temp += ".";
                                temp += date[6];
                                temp += date[7];

                                date = temp;
                            }
                            p.delete(date);
                            break;
                        }
                    case "help": p.help(); break;
                    case "quit": return;
                }
            }
        }

        public void register(String userName)
        {

            
        }

       
        public void show(String date)
        {

            String line = Url + "/schedule/show?name=" + name + "&date=" + date;               //
            string responseText = string.Empty;
       
            StringBuilder sb = new StringBuilder();
           
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(line);
            request.Method = "GET";
            request.Timeout = 30 * 1000; // 30초
            request.Headers.Add("Authorization", "BASIC SGVsbG8="); // 헤더 추가 방법

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                HttpStatusCode status = resp.StatusCode;
                //Console.WriteLine(status);  // 정상이면 "OK"

                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
            }

            String str = responseText;
            int left = 0;
            int right = 0;
            while (true)
            {
                left = str.IndexOf("content"); if (left == -1) break;
                right = str.IndexOf(","); if (right == -1) break;
                if (right <= left)
                {
                    str = str.Substring(left);
                    left = str.IndexOf("content"); if (left == -1) break;
                    right = str.IndexOf(","); if (right == -1) break;
                }
                String temp = str.Substring(left + 8, right - left - 8);
                sb.Append(temp + "\n");
                str = str.Substring(right);

            }
            Console.WriteLine(sb.ToString());
        }

        public void show()
        {

            String line = Url + "/schedule/show?name=" + name;               //
            string responseText = string.Empty;

            StringBuilder sb = new StringBuilder();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(line);
            request.Method = "GET";
            request.Timeout = 30 * 1000; // 30초
            request.Headers.Add("Authorization", "BASIC SGVsbG8="); // 헤더 추가 방법

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                HttpStatusCode status = resp.StatusCode;
               // Console.WriteLine(status);  // 정상이면 "OK"

                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
            }

            String str = responseText;
            int left = 0;
            int right = 0;
            while (true)
            {
                left = str.IndexOf("content"); if (left == -1) break;
                right = str.IndexOf(","); if (right == -1) break;
                if (right <= left)
                {
                    str = str.Substring(left);
                    left = str.IndexOf("content"); if (left == -1) break;
                    right = str.IndexOf(","); if (right == -1) break;
                }
                String temp = str.Substring(left + 8, right - left - 8);
                sb.Append(temp + "\n");
                str = str.Substring(right);

            }
            Console.WriteLine(sb.ToString());
        }

       

        public void post(String date, String data)
        {
            String line = Url + "/schedule/post?name=" + name + "&date=" + date;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(line);
            request.Method = "POST";
            data = "content=" + data;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 30 * 1000;

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            
            request.ContentLength = bytes.Length; // 바이트수 지정

            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(bytes, 0, bytes.Length);
            }


            // Response 처리
            string responseText = string.Empty;
            using (WebResponse resp = request.GetResponse())
            {
                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
            }

            Console.WriteLine(responseText);

        }

        public void list(String user)
        {
            String line = Url + "/user/list";
            string responseText = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(line);
            request.Method = "GET";
            request.Timeout = 30 * 1000; // 30초
            request.Headers.Add("Authorization", "BASIC SGVsbG8="); // 헤더 추가 방법

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                HttpStatusCode status = resp.StatusCode;
                //Console.WriteLine(status);  // 정상이면 "OK"

                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
            }

            int index = responseText.IndexOf(user);
            if (index == -1)
            {
                register(user);
                Console.WriteLine("registration complete");
            }
            else return;

        }

        public void delete(String date, int index)
        {
            String line = Url + "/schedule/delete?name=" + name + "&date=" + date + "&index=" + index;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(line);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 30 * 1000;

            // Response 처리
            string responseText = string.Empty;
            using (WebResponse resp = request.GetResponse())
            {
                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
            }

            Console.WriteLine(responseText);
        }


        public void delete(String date)
        {
            String line = Url + "/schedule/delete-all?name=" + name + "&date=" + date;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(line);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 30 * 1000;

            // Response 처리
            string responseText = string.Empty;
            using (WebResponse resp = request.GetResponse())
            {
                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
            }

            Console.WriteLine(responseText);
        }

        public void help()
        {
            Console.WriteLine("**//-------------------[MANUAL]-------------------------//**");
            Console.WriteLine("**                                                        **");
            Console.WriteLine("--show yyyy.mm.dd : show plans for the day.");
            Console.WriteLine("--show-all : show all the plans.");
            Console.WriteLine("--post yyyy.mm.dd : post plans on the day.");
            Console.WriteLine("--clear : clear console");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("powered by Choi.G.H with collaboration of Kim.J.H");
            Console.WriteLine();
        }















        

        public void Transaction(String httpMethod, String action, String user, String date, String index)   //일정등록, 일정 검색, 하루 일정 삭제, [index==-1]의 경우 단일 일정 삭제
        {
            String line = "";
            String responseText = "";
            StringBuilder sb = new StringBuilder();

            switch (action)
            {
                case "hello":line = Url + "/" + action; break;
                case "register": line = Url + "/user/register" + user; break;
                case "list": line = Url + "/user/list"; break;
                case "post": line = Url + "/schedule/" + user + date; break;
                case "show": line = Url + "/schedule/" + user + "/" + date; break;
                case "show-all": line = Url + "/schedule/" + user; break;
                case "delete": line = Url + "/schedule/" + user + "/" + date + "/" + index; break;
                case "delete-all": line = Url + "/schedule/" + user + "/" + date; break;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(line);
            request.Method = httpMethod;

            if (httpMethod == "POST")
            {
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = 30 * 1000;
            }
            else if (httpMethod == "GET")
            {
                request.Timeout = 30 * 1000; // 30초
                request.Headers.Add("Authorization", "BASIC SGVsbG8="); // 헤더 추가 방법
            }
            else if (httpMethod == "DELETE")
            {

            }

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                HttpStatusCode status = resp.StatusCode;
                //Console.WriteLine(status);  // 정상이면 "OK"

                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                    sb.Append(responseText);
                }
            }

            Console.WriteLine(sb.ToString());
            


        }

    }
    
}
