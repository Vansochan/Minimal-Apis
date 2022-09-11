namespace Engine.Entities.Base
{
    public class ResponseBase
    {
        /// <summary>
        /// Code OK is success,other failed...
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Description api response
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Response data is can be null or have value base on api
        /// </summary>
        public dynamic Data { get; set; }
        /// <summary>
        /// Api response id 
        /// </summary>
        public string RefId { get; set; }
    }
}
