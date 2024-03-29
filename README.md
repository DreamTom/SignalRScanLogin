# 使用signalr实现APP扫码登录
### 1. 背景介绍

在移动化时代，web开发很多时候都会带着移动端开发，这个时候为了减少重复输入账号密码以及安全性，很多APP端都会提供一个扫码登录功能，web端生成二维码，APP扫码直接登录web端，无需再次输入账号密码。

### 2. 实现流程图及效果图

![](https://github.com/DreamTom/SignalRScanLogin/blob/main/%E6%B5%81%E7%A8%8B%E5%9B%BE.png)
![](https://github.com/DreamTom/SignalRScanLogin/blob/main/%E6%95%88%E6%9E%9C%E5%9B%BE.gif)
### 3. 代码运行环境

- ASP.NET CORE 7.0

- VS2022

  本案例为了操作方便，并没有操作db，直接硬编码了一个账号:admin  密码:123456 来模拟用户登录流程，正式环境可以用jwt来做认证，我这里自己简单实现了一个认证方式可供参考。

  项目启动后，使用浏览器打开`http://localhost:5196/app.html`和`http://localhost:5196/web.html`分别来模拟app和web的登录，先在APP端输入账号密码登录，然后到web端生成二维码，复制到APP端扫码就可以看到扫码登录效果啦~

### 4. 总结

市面上也有很多扫码登录是通过轮询的方式来实现，也就客户端定时发送请求来查询二维码标识符的扫描状态，感觉会有一定影响，SignalR的功能当然不是止步于此啦，这里只是一个简单案例，如果有帮助的话，可以来个star哦，谢谢
