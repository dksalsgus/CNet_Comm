using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGTTest.Cls
{
    class XGT_Enum
    {
        public enum EnCmdType
        {
            /// <summary>
            /// Bit, Word 형의 직접 변수 읽기
            /// </summary>
            SS = 0,

            /// <summary>
            /// Word 형의 직접 변수를 블록 단위로 읽기 (Bit 연속 읽기는 허용되지 않습니다)
            /// </summary>
            SB
        }
    }
}
