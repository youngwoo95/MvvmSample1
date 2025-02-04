namespace WPFMVVMSample1.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// ShowDialog 메서드를 호출하여 팝업 창을 모달로 띄우고, 결과를 반환한다.
        /// </summary>
        /// <returns></returns>
        bool ShowDialog();
    }
}
