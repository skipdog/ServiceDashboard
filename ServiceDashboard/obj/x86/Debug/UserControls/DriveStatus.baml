   M S B A M L   `   `   `  ����  ServiceDashboard(    "Dashboard.UserControls.DriveStatus   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       
  yellow �� EndPoint �� 
StartPoint � Color � Offset 	 green  red��%�  &	        &	 �     &	 <    �� 5	   
   $ 0.5,1&�6-   $ 0.5,0&�6>   .��5
      � $ 	#FF6E6E00��6   $	 1.0Q�6-   � 5      $ 	#FFFFFF88��6   $ 0Q�6-   5      �� 5   
   $ 0.5,1&�6,   $ 0.5,0&�6=   .��5      � $ 	#FF023501��6   $	 1.0Q�6-   � 5      $ 	#FFB6F9B4��6   $ 0Q�6-   5      �� 5   
   $ 0.5,1&�6*   $ 0.5,0&�6;   .��5      � $ 	#FF6E0000��6   $	 1.0Q�6-   � 5      $ 	#FFFD8C8C��6   $ 0Q�6-   5      5      .��5      � .+�5   
   �� -   5         ��Name$ ProgressBarBackground��5   G   ���	#FF000000 J� RenderTransformOrigin$ 0.5,0.5&�6'   ��5      �� 5      $ 0.5,1&�6&   $ 0.5,0&�67   .��5      � $ White��6#   $ 0Q�61   � 5      $
 Gray��6#   $ 1Q�60   5      5      �� 5   
   -   5        $ ProgressBarForeground��5       ���	#FF000000$ 0.5,0.5&�5   '   $
��Left=�5    ,   ��5!      �� 5"       �� 
TargetType" ��6    �� Triggers 5#      �� 5$      	 �� Binding	 6&   �� 
 �� Path$
 
ColorIndex� �� RelativeSource ��  �� AncestorType" >� �� Value 06�   .a�5%      ��  �� Property � �� �� Value# [ 65   5&      �� 5'      	 6&   �� $
 
ColorIndex� �� " >� 16�   .a�5(      ��  � ��# [  65   5)      �� 5*      	 6&   �� $
 
ColorIndex� �� " >� 26�   .a�5+      ��  � ��# [ 65   5,      5.      50      (� 51   
   .��52      �� -   5         ��Name$ ProgressText��53   !    �� TextAlignment$ Center=�52      
������ $��Times)�6H   
���   �$��2,2,2,2q�53   7   0%54      55      56      57      