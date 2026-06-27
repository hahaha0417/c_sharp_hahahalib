namespace hahahalib
{
    /// <summary>
    /// 給 <see cref="hahaha_thread_command"/> 使用的簡單命令封裝物件。
    /// <see cref="Code_"/> 表示命令代碼，<see cref="Parameter_"/> 攜帶額外參數。
    /// </summary>
    public class hahaha_thread_command_command
    {
        public string? Code_;
        public object? Parameter_;
    }
}
