   M S B A M L   `   `   `  ????C  ?TradeWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:TradeWPFTradeWPF  5           TradeWPF.Pages.ListProduct   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:TradeWPF     5      -   5          N? Title$  ListProduct??5	       N? 
FontFamily$ Comic Sans MS)?67    ? 	Resources 5
       ?? 
TargetType ?? Triggers ?? Binding ?? Value ?? Property ?? Value??%?   '??       ?? 5   
   " ??6    5      ?? 5       6   ?? *AvailableInStock+	 false6C   .a?5      ??  ? ??
 ?????6H   5      5      5      .??5      ? 	 ? RowDefinitions	 5   
   ?? 5      6   ?? 5      $??9* ?6   6*   ?? 5      6   .+?5   
   ?? 
 ?? Orientation$
 
Horizontal=?6   $??2??6.   .+?5      ?? -   5         ??Name$ 	AddButton??5      $??5q?6(   $??5q?63   .??6W   Добавить6a   ?? 5      -   5        $ 
EditButton??5      $??5q?6)   $??5q?64   .??6Y   Редактировать6h   ?? 5      -   5        $ DeleteButton??5      $??5q?6+   $??5q?66   .??6]   Удалить6f   5      ?? 5   
   $
 
Horizontal=?6   .+?5      ?? -   5        $ AscendingButton??5      $??5q?6.   $??5q?69   .??6c   Возрастание6p   ?? 5       -   5        $ DescendingButton??5       $??5q?6/   $??5q?6:   .??6e   Убывание6o   ?? 5!      -   5         ??Name$ SearchTextBox??5!      $??5q?6-   $	??200??68   6n   ?? 5"      -   5        $??FiltrationComboBox??5"      $??5q?61   $	??200??6<   6y   5#      ?? 5$   
   -	   5         ??Name$ CountStringsLabel??5$      $??	0,0,20,15q?6+   $??Right=?6>   $??Bottom=?6Z   $??2??6u   6?   ?? 5%   
   -
   5        $??ProductsListView??5%       $??1??6   ??68   ?? *	Product+??5&      ?? 5'      .R?5(      ?  ? ColumnDefinitions 5)      ?? 5*      $??2* ?6/   6<   ?? 5+      $??4* ?6/   6<   ?? 5,      61   .+?5.      ?? $
??Fill=?6    $	??300??6O   $	??200??6[   $??Center=?6h   $??Center=?6?   ??6/   ?? *ProductPhoto+6?   ?? 5/      $??10,0,0,0q?6%   $??1??67   $??Center=?6G   $??Center=?6d   .+?50      ?? $??20(?6(   $
??Bold"?66   ??6H   ?? *ProductName+6g   ?? 51      ??6(   ?? *ProductDescription+6N   ?? 52      ??6(   ?? *ProductManufacturer+6O   ?? 53      $??20(?6(   
?????????66   ?? *ProductCost+ ?? StringFormat$ {0:C}??6?   54      ?? 55      $??20,0,0,0q?6#   $??2??6\   $??Center=?6l   $??Center=?6?    d? 	IsChecked 65   ?? *AvailableInStock+6?   56      57      59      5:      5;      