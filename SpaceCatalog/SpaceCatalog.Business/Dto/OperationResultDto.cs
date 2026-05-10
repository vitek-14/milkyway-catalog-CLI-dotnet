namespace SpaceCatalog.Business.Dto
{
    /// <summary>
    /// Represents the result of a business operation.
    /// </summary>
    public class OperationResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Creates a successful operation result.
        /// </summary>
        /// <param name="message">The result message.</param>
        /// <returns>A successful operation result.</returns>
        public static OperationResultDto Ok(string message)
        {
            return new OperationResultDto
            {
                Success = true,
                Message = message
            };
        }

        /// <summary>
        /// Creates a failed operation result.
        /// </summary>
        /// <param name="message">The result message.</param>
        /// <returns>A failed operation result.</returns>
        public static OperationResultDto Fail(string message)
        {
            return new OperationResultDto
            {
                Success = false,
                Message = message
            };
        }
    }
}
