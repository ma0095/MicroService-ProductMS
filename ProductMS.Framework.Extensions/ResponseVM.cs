using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Framework.Extensions
{

    /// <summary>
    /// Represent a Response
    /// </summary>
    public class ResponseVM
    {
        #region Member
        /// <summary>
        /// get or set Response Code
        /// </summary>
        public string? ResponseCode { get; set; }
        /// <summary>
        /// get or set Response Message
        /// </summary>
        private string? _ResponseMessage;
        /// <summary>
        /// get or set Response Mesage
        /// </summary>
        public string? ResponseMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ResponseCode))
                {
                    return string.Empty;
                }
                return string.IsNullOrWhiteSpace(ResponseCodes.ResourceManager.GetString(ResponseCode))
                    ? _ResponseMessage
                    : ResponseCodes.ResourceManager.GetString(ResponseCode);
            }

            set => _ResponseMessage = value;
        }
        #endregion
        #region Contructor
        /// <summary>
        /// Initialize Response sucess
        /// </summary>
        /// <param name="successCode"></param>
        public ResponseVM(string successCode)
        {
            ResponseCode = successCode;
        }
        public ResponseVM(string successCode, string responseMessage)
        {
            ResponseCode = successCode;
            ResponseMessage = responseMessage;
        }
        #endregion
    }
}
