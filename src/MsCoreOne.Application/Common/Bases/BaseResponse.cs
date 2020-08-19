namespace MsCoreOne.Application.Common.Bases
{
    public class BaseResponse<TOutput>
    {
        public BaseResponse()
        {

        }

        public BaseResponse(TOutput output)
        {
            Data = output;
            Errors = null;
            Message = string.Empty;
        }

        public TOutput Data { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
