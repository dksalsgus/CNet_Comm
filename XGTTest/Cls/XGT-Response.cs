using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XGTTest.Cls.XGT_Enum;

namespace XGTTest.Cls
{
    class XGT_Response
    {

        public class ResponseAck : XGT_Response
        {

            /// <summary>
            /// 헤더
            /// </summary>
            public byte Header { get; set; }
            /// <summary>
            /// 국번
            /// </summary>
            public string NationalNo { get; set; }
            /// <summary>
            /// 명령어
            /// </summary>
            public string Cmd { get; set; }
            /// <summary>
            /// 명령어 타입
            /// </summary>
            public string CmdType { get; set; }
            /// <summary>
            /// 블록 수
            /// </summary>
            public string Block { get; set; }
            /// <summary>
            /// 데이터 개수
            /// </summary>
            public string DataSize { get; set; }
            /// <summary>
            /// 데이터
            /// </summary>
            public string DataName { get; set; }
            /// <summary>
            /// 테일
            /// </summary>
            public byte Tail { get; set; }
        }

        public class ResponseNak : XGT_Response
        {
            /// <summary>
            /// 헤더
            /// </summary>
            public byte Header { get; set; }
            /// <summary>
            /// 국번
            /// </summary>
            public string NationalNo { get; set; }
            /// <summary>
            /// 명령어
            /// </summary>
            public string Cmd { get; set; }
            /// <summary>
            /// 명령어 타입
            /// </summary>
            public string CmdType { get; set; }
            /// <summary>
            /// 에러코드
            /// </summary>
            public string ErrorCode { get; set; }
            /// <summary>
            /// 테일
            /// </summary>
            public byte Tail { get; set; }
        }
    }
}
