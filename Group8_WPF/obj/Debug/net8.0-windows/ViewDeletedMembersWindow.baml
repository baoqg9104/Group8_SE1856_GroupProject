   M S B A M L   `   `   `  ����E  AGroup8_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Group8_WPF
Group8_WPF  5   	   )    #Group8_WPF.ViewDeletedMembersWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Group8_WPF     5   	   D @ModernWpf, Version=0.9.6.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.6.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5   	   -   5          ModernWpf.ThemeManager    RequestedTheme$  Light=�5	   	    � Title$ View Deleted Members��5
   	    � WindowStartupLocation$ CenterScreen=�5   	   $	��750��5   	   $
��1150��5   	    � 	Resources 5          Group8_WPF.GenderConverter   GenderConverter��%   &	          5   
   6    5      .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    $��81��5       �� 5      .+�5      � $��Center=�6   $��34��5      5"      &   System.Windows.Controls.DataGrid  5#      -   5          Name$ DeletedMembersDataGrid��5#      $��1��5$      $��	0,0,10,10q�5%      ���#FAFAFA   AutoGenerateColumns$ False��5'         CanUserAddRows$ False��5(         
IsReadOnly$
 True��5)      	   SelectionMode$	 Single=�5*      
   	CellStyle
 5+      �� 5,       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   .�5-      ��  �� Property � �� �� Value$ 0,0,1,1q�5.   !   �� 5/       � ��
 �����50   !   �� 51       � ��$ 5q�52   !   53       55      =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%  '        �� 56      "  6   .�57      ��  � ��$ 30��58   !   �� 59       � �� �#007ACC5:   !   �� 5;       � ��
 �����5<   !   �� 5=       � ��$ SemiBold"�5>   !   �� 5?       � ��$ Center=�5@   !   5A      5B         Columns 5C      4  .System.Windows.Controls.DataGridTemplateColumn  5D      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Member ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.5* 5E   -      CellTemplate 5F      �� 5G      .R�5H   "   �� $
��Wrap=�5I   ,    �� Padding$ 5q�5J   ,   ��5H   ,   �� *
MemberId+5J   ,   5K      5M        5N      $ Member Name��6-   $ * 5O   -    5P      �� 5Q      .R�5R   "   �� $
��Wrap=�5S   ,   $ 5q�5T   ,   ��5R   ,   �� *	User.Name+5T   ,   5U      5W        5X      $ Email��6-   $ * 5Y   -    5Z      �� 5[      .R�5\   "   �� $
��Wrap=�5]   ,   $ 5q�5^   ,   ��5\   ,   �� *
User.Email+5^   ,   5_      5a        5b      $ Password��6-   $ * 5c   -    5d      �� 5e      .R�5f   "   �� $
��Wrap=�5g   ,   $ 5q�5h   ,   ��5f   ,   �� *User.Password+5h   ,   5i      5k        5l      $ Date of birth��6-   $ * 5m   -    5n      �� 5o      .R�5p   "   �� $
��Wrap=�5q   ,   $ 5q�5r   ,   ��5p   ,   �� *DateOfBirth+ �� StringFormat$ {0:dd/MM/yyyy}��5r   ,   5s      5u        5v      $ Phone��6-   $ * 5w   -    5x      �� 5y      .R�5z   "   �� $
��Wrap=�5{   ,   $ 5q�5|   ,   ��5z   ,   �� *Phone+5|   ,   5}      5        5�      $ Gender��6-   $ * 5�   -    5�      �� 5�      .R�5�   "   �� $
��Wrap=�5�   ,   $ 5q�5�   ,   ��5�   ,   �� *Gender+ �� 	Converter# [  5�   ,   5�      5�        5�      $ Status��6-   $ * 5�   -    5�      �� 5�      .R�5�   "   �� $
��Wrap=�5�   ,   $ 5q�5�   ,   ��5�   ,   �� *User.Status+5�   ,   5�      5�      5�      5�      5�      5�      