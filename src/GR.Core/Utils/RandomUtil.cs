using System.Text;

namespace GR.Utils
{
    /// <summary>
    /// 随机数工具
    /// </summary>
    public class RandomUtil
    {
        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandom(int length = 4)
        {
            var letters = "1,2,3,4,5,6,7,8,9,0,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            return GetRandom(letters, length: length);
        }

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomPwd(int length = 8)
        {
            var letters = "1,2,3,4,5,6,7,8,9,0,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,~,!,@,#,$,%,^,&,*,(,),_,+";
            return GetRandom(letters, length: length);
        }

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="letters"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandom(string letters, int length = 4)
        {
            var dataArr = letters.Split(',');
            var random = new Random();
            var code = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var d = dataArr[random.Next(dataArr.Length)];
                code.Append(d);
            }
            return code.ToString();
        }
    }
}
