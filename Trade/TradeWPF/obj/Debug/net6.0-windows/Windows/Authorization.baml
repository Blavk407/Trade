   M S B A M L   `   `   `  ????C  ?TradeWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TradeWPF.WindowsTradeWPF.Windows  5   	   $    TradeWPF.Windows.Authorization   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:TradeWPF.Windows     5   	     ? Title$  Авторизация??5   	   $	??700??6   $
??1600??6*   $??Comic Sans MS)?67   $??40(?6R   .??5	      ?  ? ColumnDefinitions 5
   
   ?? 5      6!   ?? 5      6!   .+?5   
   ?? $??\Resources\logo.png??6   $
??None=?6-   $	??Top=?6<   $??0,20,0,0q?6T   6h   ?? 5   
   $??55(?6   $??Bottom=?6   $??Center=?69   $??0,0,0,70q?6V   .??6h   *(ООО "Столовые приборы"6?   ?? 5   
   -   5        $??Center=?5      $??30(?61   $??Bottom=?6?   $??30q?6Z   
????  ? ? Cursor$
 Hand??6x   /-Просмотреть ассортимент6?   ?? 5   
   $??Center=?6   $??Center=?62   $??1??6M   .+?5      ??  ?? Orientation$ 
Horizontal=?6   .+?5      ?? $ Vertical=?6   .+?5      ?? $??0,0,0,20q?6    
Логин69   ?? 5      $??0,0,0,20q?6    Пароль6:   5      ?? 5      $ Vertical=?6   $??10,0,0,0q?64   .+?5      ?? -   5        $??LoginTextBox??5   <   $	??450??6   $??0,0,0,20q?6*   6R   G? 5      -   5        $??PasswordPasswordBox??5   @   $	??450??6"   $??0,0,0,20q?6.   6]   5      5      ? 5      .+?5      ?? .+?5      ?? -   5        $??CaptchaImage??5      $y?Hidden=?60   $	??400??6D   $	??150??6P   $??\Resources\captcha.jpg??6]   6   ?? 5      -   5        $??CaptchaTextBox??5      $y?Hidden=?64   $	??250??6H   $??0,10,0,0q?6T   6h   5       ?? 5!      -   5        $??
AuthButton??5!   %   $??75??6   $	??Top=?6U   .??6m   
Войти6t   5"      5#      5$      5%      