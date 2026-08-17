using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Framework.Extensions
{

    /// <summary>
    /// Represent a Action handler
    /// </summary>
    public class ActionStatus
    {
        #region Member
        /// <summary>
        /// get or set IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; } = false;
        /// <summary>
        /// get or set Error
        /// </summary>
        public ResponseVM? Response { get; set; }
        /// <summary>
        /// get or set Exception
        /// </summary>
        public Exception? Exception { get; set; }
        /// <summary>
        /// get or set exception status
        /// </summary>
        public bool HasException => Exception != null;
        #endregion
        #region Constructor
        /// <summary>
        /// Initialize Active Status
        /// </summary>
        /// <param name="isSuccess"></param>
        public ActionStatus(bool isSuccess, ResponseVM response)
        {
            IsSuccess = isSuccess;
        }
        /// <summary>
        /// Initialize Active Status
        /// </summary>
        /// <param name="error">Error</param>
        public ActionStatus(ResponseVM error)
        {
            IsSuccess = false;
            Response = error;
        }
        /// <summary>
        /// Initialize Active status
        /// </summary>
        /// <param name="exception">Exception</param>
        public ActionStatus(string locationCode, Exception exception)
        {
            IsSuccess = false;
            Exception = exception;
            Response = new ResponseVM(locationCode);
        }
        /// <summary>
        /// Initialize Active status
        /// </summary>
        /// <param name="actionStatus"></param>
        public ActionStatus(ActionStatus actionStatus)
        {
            IsSuccess = actionStatus.IsSuccess;
            Exception = actionStatus.Exception;
            Response = actionStatus.Response;
        }
        #endregion
        #region Operators
        /// <summary>
        /// Get boolean response
        /// </summary>
        /// <param name="actionStatus">Active status</param>
        public static implicit operator bool(ActionStatus actionStatus)
        {
            return actionStatus.IsSuccess;
        }
        /// <summary>
        /// Get Active status reponse
        /// </summary>
        /// <param name="isSuccess">boolean</param>
        public static explicit operator ActionStatus(bool isSuccess)
        {
            return new ActionStatus(isSuccess, new ResponseVM("DEFAULT"));
        }
        #endregion
    }
    /// <summary>
    /// Represent a Generic Action Handler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionStatus<T> : ActionStatus
    {
        #region Member
        /// <summary>
        /// Represent generic response
        /// </summary>
        public T? Result { get; set; }
        /// <summary>
        /// get or set TotalCount
        /// </summary>
        public int TotalCount { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initialize success reponse with Data
        /// </summary>
        /// <param name="isSuccess">Boolean</param>
        /// <param name="result">Result</param>
        public ActionStatus(bool isSuccess, T result) : base(isSuccess, new ResponseVM("DEFAULT"))
        {
            Result = result;
        }
        /// <summary>
        /// Initialize success reponse with list Data
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="result"></param>
        /// <param name="totalCount"></param>
        public ActionStatus(bool isSuccess, T result, int totalCount) : base(isSuccess, new ResponseVM("DEFAULT"))
        {
            Result = result;
            TotalCount = totalCount;
        }
        /// <summary>
        /// Initialize error response 
        /// </summary>
        /// <param name="error">Error</param>
        public ActionStatus(ResponseVM error) : base(error)
        {
            Result = default;
        }
        /// <summary>
        /// Initialize Exception response
        /// </summary>
        /// <param name="exception">Exception</param>
        public ActionStatus(string locationCode, Exception exception) : base(locationCode, exception)
        {
            Result = default;
        }
        public ActionStatus(ActionStatus actionStatus) : base(actionStatus)
        {
            Result = default;
        }
        #endregion
    }
}
