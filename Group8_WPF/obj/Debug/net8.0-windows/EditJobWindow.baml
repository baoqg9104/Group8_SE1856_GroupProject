   M S B A M L   `   `   `  ����E  AGroup8_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Group8_WPF
Group8_WPF  5   	       Group8_WPF.EditJobWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Group8_WPF     5   	   D @ModernWpf, Version=0.9.6.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.6.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5   	     ModernWpf.ThemeManager    RequestedTheme$  Light=�5	   	   $	��450��5
   	   $
��1100��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	   .��5      � $��20,20,20,21q�6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��
20,20,20,0q�5      .+�5      �  � ColumnDefinitions 5      �� 5      $
��278* �6'   �� 5      $
��186* �6'   �� 5      $
��207* �6'   �� 5      $
��419* �6'    � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   .+�5      �� $��0��6   $��0��5      $��0,0,10,0q�5       .+�5!      �� $��
Job Title:��6   $��Center=�5"      �� 5#      -   5         ��Name$ JobTitleTextBox��5#      $��0,5,0,0q�5$      5%      �� 5&      $��0��6   $��1��5'      $��5,0,5,0q�5(      .+�5)      �� $��Salary:��6   $��Center=�5*      �� 5+      -   5        $ SalaryTextBox��5+      $��0,5,0,0q�5,      5-      �� 5.      $��1��6   $��2��5/      $��
10,10,0,10q�50      .+�51      �� $��Application Deadline:��6   $��Center=�52      (  "System.Windows.Controls.DatePicker  53      -   5          Name$ DeadlineDatePicker��53   !   $��0,5,0,0q�54   !   55      �� 56      $��1��6   $��0��57      $��	0,10,10,0q�58      .+�59      �� $��Description:��6   $��Center=�5:      �� 5;      -   5        $ DescriptionTextBox��5;       �� TextWrapping$
 Wrap=�5<      $��80��5=      $��0,5,0,0q�5>      5?      �� 5@      $��1��6   $��1��5A      $��5,10,5,0q�5B      .+�5C      �� $��	Location:��6   $��Center=�5D      �� 5E      -   5        	 ��Name$	 LocationComboBox��5E      $��0,5,0,0q�5F      $	��149��5G      5H      �� 5I      $��0��6   $��2��5J      $��	10,0,0,10q�5K      .+�5L      �� $��Currency Code:��6   $��Center=�5M      �� 5N      -   5        $	 CurrencyComboBox��5N      $��0,5,0,0q�5O      .@�5P      �� $	��VND��6'   �� 5Q      $	��USD��6'   5R      5S      �� 5T      $��0��6   $��3��5U      $��3��5V      .+�5W      �� $��Skills:��6   $��Center=�5X      �� 5Y      -   5        
 ��Name$
 SkillListBox��5Y      $��10q�5Z      $��10(�5[      $	��190��5\      ��5]      �� 5^      .R�5_   "   �� -   5        ��5_   +   �� *	SkillName+ d� 	IsChecked 5`   +   �� *
IsSelected+5b   +   5c      5e      5f      �� 5g      $��2��6   $��1��5h      $��2��5i      .+�5j      �� $��10,0,0,0q�6   .��6.   Skills66   �� 5k      -	   5         ��Name$ SelectedSkillsTextBlock��5k       $��10q�5l       $
��Wrap=�5m       5n      5o      5p      �� 5r   
   -
   5        $
��Save��5r      $��72��5s      $��428,329,0,0q�5t      $	��Top=�5u      $��34��5v      5w      5x      5y      