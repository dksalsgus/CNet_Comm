using System;
using System.Collections.Generic;
using System.Text;

namespace HPLC.Net.TCP
{
    /// <summary>
    /// Socket Receive 패킷을 클래스로 Serialize 할 메서드 대리자
    /// </summary>
    public delegate byte[] PacketSerializer();

    /// <summary>
    /// Socket Receive 패킷을 클래스로 Deserialize 할 메서드 대리자
    /// </summary>
    public delegate bool PacketDeserializer(byte[] data);

    /// <summary>
    /// Socket 전송 후 결과
    /// </summary>
    public enum TxResult
    {
        /// <summary>
        /// 성공
        /// </summary>
        Success,

        /// <summary>
        /// 이미 요청중
        /// </summary>
        AlreadyRequest,

        /// <summary>
        /// 이미 접속된 장치(SMS 요청을 해야하는지?)
        /// </summary>
        AlreadyConnected,

        /// <summary>
        /// 소켓이 연결되지 않은 경우
        /// </summary>
        NotConnected,

        /// <summary>
        /// 오류 발생
        /// </summary>
        ExceptionRaised,

        /// <summary>
        /// 시간내 응답없음
        /// </summary>
        ResponseTimeout,

        /// <summary>
        /// Deserialize 오류
        /// </summary>
        ErrorDeserialize
    }

    public enum PacketError
    {
        None,
        STX,
        SIZE,
        LENGTH,
        CRC,
        CHKSUM,
        CHKXOR,
        ETX
    }
}
