   M S B A M L   `   `   `  ����   
NI_torpedo     NI_torpedo.View.GameWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$  Torpedo��$	��470��$	��850�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � Icon$ /Images/torpedo.png������  �� ImageSource$ /Images/sea.jpg��.���� .+��� 
���   � �� Orientation$ 
Horizontal=�.+��� -   $��15, 0, 0, 0q�
�������$��F1: help��$
��Left=��� $��15, 0, 0, 0q�
�������$+��%Shift + F2: Al hajóinak megmutatása��$
��Left=��� $ 
Horizontal=�.+��� -    ��Name$ nevLabel��$
��Left=�$
��Bold"�
���  ���� -   $ kovetkezoPlayer_label��$��Következő játékos:��$��250, 0, 0, 0q�$y�Hidden=��� -   $ kovetkezoPlayer��$
��Bold"��� $ 
Horizontal=�.+��� $��Al játéktábla:��$��25, 0, 0, 0q�$��Bottom=�$
��Bold"�$��14(��� $��Saját játéktábla:��$��244, 0, 0, 0q�$��Bottom=�$
��Bold"�$��14(��� -    ��Name$! nehezseg_valasztas_combobox��$	��140�� �� SelectedIndex$ 0��$y�Visible=�$��206, 0, 0, 5q�$��Center=�.@��� $��válassz nehézséget...��$y�	Collapsed=��� $��könnyű���� $��nehéz���� $ 
Horizontal=�.+��� -   	 ��Name$	 masik_player_jatektabla��
���   �$	��300��$	��300��$��	25,0,25,0q��� -   $	 sajat_jatektabla��
���   �$	��300��$	��300���� -	   
 ��Name$
 hajok_elhelyezese��$ 
Horizontal=�$y�Visible=�$��10, 0, 0, 0q�.+��� .+��� -
   $	 hajo21��$��60��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� -   $	 hajo22��$��60��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� -   $	 hajo23��$��60��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� -   $	 hajo24��$��60��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� .+��� -   $	 hajo31��$��90��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� -   $	 hajo32��$��90��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� -   $	 hajo33��$��90��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� .+��� -   $	 hajo34��$��90��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� -   $	 hajo41��$	��120��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� .+��� -   $	 hajo42��$	��120��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� -   $	 hajo51��$	��150��$��30��$��0, 0, 10, 10q�
���   �$y�Visible=��� -   $
 eredmenyjelzo��$ Vertical=�$y�	Collapsed=�$��10, 0, 0, 0q�.+��� $ 
Horizontal=�.+��� $��Körök száma: ���� -   $ korok_szama��$��0��$��12, 0, 0, 0q��� $ 
Horizontal=�.+��� $��Saját találatok: ���� -   $ sajat_talalatok��$��0��$��
5, 0, 0, 0q��� $ 
Horizontal=�.+��� $��Ellenfél találatai:���� -   $ ellenfel_talalatai��$��0���� $��Elsülyesztett:���� $ 
Horizontal=�.+��� $��2-es hajók:���� -   $ hajo2��$��0���� $	��/ 4���� $ 
Horizontal=�.+��� $��3-as hajók:���� -   $ hajo3��$��0���� $	��/ 4���� $ 
Horizontal=�.+��� $��4-es hajók:���� -   $ hajo4��$��0���� $	��/ 2���� $ 
Horizontal=�.+��� $��5-ös hajók:���� -   $ hajo5��$��0���� $	��/ 1���� $ 
Horizontal=�$��0, 12, 0, 0q�$��31��.+��� -    ��Name$$ sajat_jatektabla_mentes_button��$��Mentés��$��240, 0, 55, 0q�$��16(�$
��Bold"����	#FF181FA3���	#FFC5BBBB$��83���� -   $$ sajat_jatektabla_torles_button��$��Törlés��$��16(�$
��Bold"����	#FF181FA3���	#FFC5BBBB$��81��z�V� .~��� �� ��  �� Angle$ -1.365Q�T� �� -   $
 exit��$��Feladás��$��16(�$
��Bold"�$��55, 0, 0, 0q����	#FF181FA3���	#FFC5BBBB$��91���� -    $ kesobb_folyt��$��Későbbi folytatás��$��16(�$
��Bold"�$��50, 0, 0, 0q����	#FF181FA3���	#FFC5BBBB$	��158��