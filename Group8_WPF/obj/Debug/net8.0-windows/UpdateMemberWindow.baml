   M S B A M L   `   `   `  ����E  AGroup8_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Group8_WPF
Group8_WPF  5   	   #    Group8_WPF.UpdateMemberWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Group8_WPF     5   	   D @ModernWpf, Version=0.9.6.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.6.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5   	     ModernWpf.ThemeManager    RequestedTheme$  Light=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � Title$ 
Add Member��5   	   $	��450��5   	   $	��500��5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5'   
   �� $��1��6   $��0��5(      $��Member Name:��5)      $��	0,10,10,0q�5*      �� 5+   
   -   5         ��Name$ MemberNameTextBox��5+      $��1��5,      $��1��5-      $��0,10,0,0q�5.      �� 5/   
   $��2��6   $��0��50      $��Email:��51      $��	0,10,10,0q�52      �� 53   
   -   5        $ EmailTextBox��53      $��2��54      $��1��55      $��0,10,0,0q�56      �� 57   
   $��3��6   $��0��58      $��	Password:��59      $��	0,10,10,0q�5:      �� 5;   
   -   5        $ PasswordTextBox��5;      $��3��5<      $��1��5=      $��0,10,0,0q�5>      �� 5?   
   $��4��6   $��0��5@      $��Date of Birth:��5A      $��	0,10,10,0q�5B      (  "System.Windows.Controls.DatePicker  5C   
   -   5          Name$ DateOfBirthDatePicker��5C      $��4��5D      $��1��5E      $��0,10,0,0q�5F      �� 5G   
   $��5��6   $��0��5H      $��Phone:��5I      $��	0,10,10,0q�5J      �� 5K   
   -   5        $ PhoneTextBox��5K      $��5��5L      $��1��5M      $��0,10,0,0q�5N      �� 5O   
   $��6��6   $��0��5P      $��Gender:��5Q      $��	0,10,10,0q�5R      �� 5S   
   -   5         ��Name$ GenderComboBox��5S      $��6��5T      $��1��5U      $��0,10,0,0q�5V      .@�5W      �� .��6   Male6!   �� 5X      .��6   Female6#   5Y      �� 5Z   
   -   5        $��9��5Z      $��1��5[      $
��Save��5\      $��	81,33,0,0q�5]      $	��Top=�5_      $��76��5`      $��38��5a      5b      5c      