   M S B A M L   `   `   `  ����   
NI_torpedo&     NI_torpedo.View.ScoreBoardWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Torpedo��$	��430��$	��600�� � 
ResizeMode$ NoResize=� � Icon$ /Images/torpedo.png�� � WindowStartupLocation$ CenterScreen=�����  �� ImageSource$ /Images/blue.jpg��.���� .+��� -    ��Name$ 	nincsfile��$y�	Collapsed=�.+��� $'��!Jelenleg nincs mentett eredmény!��
���  ��$��20(�$
��Bold"�$��Center=��� -   $ vanfile��$y�Visible=�.+���  �� Orientation$ 
Horizontal=�$��0, 10, 0, 0q�.+��� $��1. játékos neve neve: ��$
��Bold"�$��25, 0, 0, 0q�$��14(��� -    ��Name$ player1_name��$�� �����	#FF04045C$��14(��� $��2. játékos neve: ��$
��Bold"�$��50, 0, 0, 0q�$��14(��� -   $ player2_name��$�� �����	#FF04045C$��14(��� $��	Nyertes: ��$
��Bold"�$��50, 0, 0, 0q�$��14(��� -   $ nyertes_name��$�� ��$
��Bold"�
��� � �$��14(��� $ 
Horizontal=�.+��� $��Körök száma: ��$��25, 0, 0, 0q�$
��Bold"��� -   $ 
korok_szam��$�� ���� -   $ player1_talalatai��$��Player1 találatai: ��$��50, 0, 0, 0q�$
��Bold"��� -   $ player1_talalat��$�� ���� -	   $ player2_talalatai��$��Player2 találatai: ��$��50, 0, 0, 0q�$
��Bold"��� -
   $ player2_talalat��$�� ���� $ 
Horizontal=�$��0,20,0,0q�$	��150��.+��� $ Vertical=�$��50, 0, 0, 0q�.+��� -   $ player1_hajoi��$%��Player1 elsülyesztett hajói: ��$
��Bold"��� $ 
Horizontal=�.+��� $��2-es hajó (max = 4): ��$��20, 0, 0, 0q�$
��Bold"��� -   $ player1_hajo2��$�� ���� $ 
Horizontal=�.+��� $��3-es hajó (max = 4): ��$��20, 0, 0, 0q�$
��Bold"��� -   $ player1_hajo3��$�� ���� $ 
Horizontal=�.+��� $��4-es hajó (max = 2): ��$��20, 0, 0, 0q�$
��Bold"��� -   $ player1_hajo4��$�� ���� $ 
Horizontal=�.+��� $��5-es hajó (max = 1): ��$��20, 0, 0, 0q�$
��Bold"��� -   $ player1_hajo5��$�� ���� $ Vertical=�$��100, 0, 0, 0q�.+��� -   $ player2_hajoi��$%��Player2 elsülyesztett hajói: ��$
��Bold"�$��30��z�V� .~��� ��  �� AngleX$ 3.768Q��� T� 	 T� X$	 0.855Q��� $ 
Horizontal=�.+��� $��2-es hajó (max = 4): ��$��20, 0, 0, 0q�$
��Bold"�$��30���� -   $ player2_hajo2��$�� ���� $ 
Horizontal=�.+��� $��3-es hajó (max = 4): ��$��20, 0, 0, 0q�$
��Bold"�$��30���� -   $ player2_hajo3��$�� ���� $ 
Horizontal=�.+��� $��4-es hajó (max = 2): ��$��20, 0, 0, 0q�$
��Bold"��� -   $ player2_hajo4��$�� ���� $ 
Horizontal=�.+��� $��5-es hajó (max = 1): ��$��20, 0, 0, 0q�$
��Bold"�$��30���� -   $ player2_hajo5��$�� ���� $ 
Horizontal=�$��0, 25, 0, 0q�.+��� -   
 ��Name$
 elozo_button��${�False��$��Előző��$��25, 0, 175, 0q�$��50��$��25�����	#FF181FA3���	#FFC5BBBBz�V� .~��� �� ��  �� Angle$ 1.242Q�T� �� -   $ oldal_szam_label��$��1���� -   $
 kovetkezo_button��${�False��$��Következő��$��175, 0, 0, 0q�$��25��$��71�����	#FF181FA3���	#FFC5BBBB�� -   $��Törlés��$��Center=�$��
0, 0, 0, 0q�$��25��$��72�����	#FF181FA3���	#FFC5BBBB�� -   $��	Kilépés��$��Center=�$��0, 30, 0, 0q�$��72��$��25�����	#FF181FA3���	#FFC5BBBB