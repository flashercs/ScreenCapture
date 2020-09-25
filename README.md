# ScreenCapture
Winform Screen Capture，Tencent Screen Capture



截屏功能，类似QQ的截屏界面，相似度非常高。



多显示器支持：

经测试，两个显示器是没问题的

三个以上显示器没测试过。





使用：
FrmCapture frm = new FrmCapture();

frm.CaptureFinished += CaptureFinished;

frm.Show();

