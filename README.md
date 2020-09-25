# ScreenCapture
Winform Screen Capture，Tencent Screen Capture



截屏功能，类似QQ的截屏界面，相似度非常高。



多显示器支持：

经测试，两个显示器是没问题的（前提是主显示器在左边）

三个以上显示器没测试过。

原理是截屏是在FrmCapture 窗口范围内，如果主显示器在右边，那么FrmCapture 窗口最大化了也只能显示在主显示器上；而如果主显示器在左边，那么FrmCapture 最大化的时候窗体是横跨两个显示器的。

盼建议



使用：
FrmCapture frm = new FrmCapture();

frm.CaptureFinished += CaptureFinished;

frm.Show();

