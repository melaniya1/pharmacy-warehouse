   M S B A M L   `   `   `  ����?  ;Shop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ShopShop  5          	Shop.edit   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:Shop     5        N� 
Background  �#95DBC1 N� Title$
 edit��5	      .��5      �� $
��Auto=�6   .��5   
   � $	��430��6   .+�5      �� -   5         ��Name$ stackBox��5      $��	0 20 0 30q�6+   .+�5      �� $��35(�6   $��SemiBold"�6&   $%��Изменение товара��6<   $��Center=�6W   $��Center=�6t   ���	#FF47695D6�   �� 5      $
��Left=�6   $��1,10,0,0q�67   $
��Wrap=�6I   $��SemiBold"�6]   $!��Номер продукта��6s   $	��Top=�6�   $��17(�6�   ���	#FF47695D6�   �� 5      -   5         ��Name$ IdBox��5      $��0,5,0,0q�6)   $��25(�6:      textBoxMain#��[  6H   ���	#FFD8EEDD���	#FF2E2E2E6�   �� 5      -   5        $ boxes��5      $y�	Collapsed=�6,   .+�5      �� $
��Left=�6    $��1,10,0,0q�6;   $
��Wrap=�6M   $��SemiBold"�6a   $#��Название товара��6w   $	��Top=�6�   $��17(�6�   ���	#FF47695D6�   �� 5      -   5        $ nameBox��5      $��0,5,0,0q�6/   $��25(�6@   #��[  6N   ���	#FFD8EEDD���	#FF2E2E2E6�   �� 5      $
��Left=�6    $��1,10,0,0q�6;   $
��Wrap=�6M   $��SemiBold"�6a   $#��Название аптеки��6w   $	��Top=�6�   $��17(�6�   ���	#FF47695D6�   �� 5      -   5        $ 	namePharm��5      $��0,5,0,0q�61   $��25(�6B   #��[  6P   ���	#FFD8EEDD���	#FF2E2E2E6�   �� 5      $
��Left=�6    $��1,10,0,0q�6;   $
��Wrap=�6M   $��SemiBold"�6a   $��Кол-во тавара��6w   $	��Top=�6�   $��17(�6�   ���	#FF47695D6�   �� 5      -   5        $ 	amountBox��5      $��0,5,0,0q�61   $��25(�6B   #��[  6P   ���	#FFD8EEDD���	#FF2E2E2E6�   �� 5      $
��Left=�6    $��1,10,0,0q�6;   $
��Wrap=�6M   $��SemiBold"�6a   $��
Адрес��6w   $	��Top=�6�   $��17(�6�   ���	#FF47695D6�   �� 5      -   5        $ 
addressBox��5      $��0,5,0,0q�62   $��25(�6C   #��[  6Q   ���	#FFD8EEDD���	#FF2E2E2E6�   5      �� 5      -   5         ��Name$ errorAmount��5      $>��8* Кол-во товара введено неверно��61   $��0,15,0,0q�6X   $
��Wrap=�6j   $	��Top=�6~   ���	#FFFA4848 �� TextAlignment$ Justify=�6�   $
��13.5(�6�   $y�	Collapsed=�6�   �� 5      -	   5        $ errorId��5      $K��E* Товара с таким номером не существует��6-   $��0,15,0,0q�6[   $
��Wrap=�6m   $	��Top=�6�   ���	#FFFA4848$ Justify=�6�   $
��13.5(�6�   $y�	Collapsed=�6�   �� 5      -
   5        $ 	errorGood��5      $h��b* Товар с таким названием уже существует в этой аптеке��6/   $��0,15,0,0q�6m   $
��Wrap=�6   $	��Top=�6�   ���	#FFFA4848$ Justify=�6�   $
��13.5(�6�   $y�	Collapsed=�6�   � 5      -   5         �Name$ buttons��5      $��0 15 0 0q�6(    � ColumnDefinitions 5       �� 5!      $	��25* �6+   �� 5"      $	��18* �6+   .+�5$      �� -   5         ��Name$ btnBack��5$      $��
Назад��6.   $
��Left=�6>   $��1,0,0,0q�6Y   $	��Top=�6j     btnEmpty#��[ 6�   6�   �� 5%      -   5        $ btnComplite��5%   /   {�.  $��Применить��6D   $��Right=�6X   $��0,0,0.4,-0.2q�6t   $	��Top=�6�     btnFill#��[ 6�   $��1��6�   5&      5(      5*      5+      5,      