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

        public enum EnErrorCode
        {
            /// <summary>
            /// 0003 블록수 초과 에러 개별 읽기/쓰기 요청시 블록 수가 16 보다 큼 01rSS1105%MW10…
            /// </summary>
            블록수초과에러 = 0003,
            /// <summary>
            /// 0004 변수 길이 에러 변수 길이가 최대 크기인 16 보다 큼 01rSS0113%MW10000000000…
            /// </summary>
            변수길이에러 = 0004,
            /// <summary>
            /// 0007 데이터 타입 에러 X, B, W, D, L 이 아닌 데이터 타입을 수신했음 01rSS0105%MK10
            /// </summary>
            데이터타입에러 = 0007,
            /// <summary>
            /// 0011 데이터 에러 변수의 영역 값이 잘못된 경우 01rSS0105%MW^&
            /// 데이터 길이 영역 정보가 잘못된 경우 01rSB05%MW10%4
            /// %로 시작해야 하지 않은 경우 01rSS0105$MW10
            /// 변수의 영역 값이 잘못된 경우 01rSS0105%MW^&
            /// Bit 쓰기인 경우, 반드시 00 또는 01 로 써야 하는데 다른 값으로 쓴 경우
            /// </summary>
            데이터에러 = 0011,
            /// <summary>
            /// 0090 모니터 실행 에러 등록 안된 모니터 실행을 요구한 경우
            /// </summary>
            모니터실행에러1 = 0090,
            /// <summary>
            /// 0190 모니터 실행 에러 등록 번호 범위를 초과한 경우
            /// </summary>
            모니터실행에러2 = 0190,
            /// <summary>
            /// 0290 모니터 등록 에러 등록 번호 범위를 초과한 경우
            /// </summary>
            모니터실행에러3 = 0290,
            /// <summary>
            /// 1232 데이터 크기 에러
            /// 한번에 최대 60Word 까지 읽거나 쓸 수 있는데
            /// 초과해서 요청한 경우 01wSB05%MW1040AA5512,..
            /// </summary>
            데이터크기에러 = 1232,
            /// <summary>
            /// 1234 여유 프레임 에러 필요 없는 내용이 추가로 존재하는 경우 01rSS0105%MW10000
            /// </summary>
            여유프레임에러 = 1234,
            /// <summary>
            /// 1332 데이터 타입
            /// 불일치 에러
            /// 개별 읽기/쓰기인 경우, 모든 블록은 동일한
            // 데이터 타입에 대해 요구해야 함. 01rSS0205%MW1005%MB10
            /// </summary>
            데이터타입불일치에러 = 1332,
            /// <summary>
            /// 1432 데이터 값 에러 데이터 값이 Hex 변환 불가능한 경우 01wSS0105%MW10AA%5 
            /// </summary>
            데이터값에러 = 1432,
            /// <summary>
            /// 7132 변수 요구 영역
            /// 초과 에러
            /// 각 디바이스별 지원하는 영역을 초과해서 요구한 경우 01rSS0108%MWFFFFF
            /// </summary>
            변수요구영역초과에러 = 7132,
        }
    }
}
