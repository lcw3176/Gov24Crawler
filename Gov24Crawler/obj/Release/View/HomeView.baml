   M S B A M L   `   `   `  ���� G  CGov24Crawler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Gov24Crawler.ViewGov24Crawler.View       Gov24Crawler.View.HomeView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:Gov24Crawler.View     .���   � RowDefinitions  �� $��50 ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�$��Center=�.+��� $��파일 설정��
�������$	��100��$��40��$��15(�$��2q�$��20, 0q����� *
fileButton+����  �� ElementName$ homeTab�� � 	Resources  �� 
TargetType �� Property �� Value��%:   '��       �� " ��.���  �@��CornerRadius$ 20���� $��발급 진행��
�������$	��100��$��40��$��2q�$��15(����� *processButton+���� $ homeTab�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 20���� -    ��Name$ homeTab��$��1��.@��� $��파일 설정��$y�	Collapsed=�.���   �� $��60 ��� $��* ��� $��70 �.+��� $��0��$ 
Horizontal=�$
��Left=�$��Center=�$��10q�.+��� $��파일 선택��$	��100��$��40��$��14(�
�������$��0q����� *setExcelPath+ ��%:   '��       �� " ��.���  �@��CornerRadius$ 20����  �� 
IsReadOnly . $	��550��	 �� TextWrapping$	 WrapWithOverflow=�$��10, 0q�$��15(�$��Center=����� *	excelPath+
 �� UpdateSourceTrigger$
 PropertyChanged=�� $��1��  �� $��50 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 � � ColumnDefinitions �� $	��150 ��� $	��200 ��� $��* �.+��� $	��100��$��40��
������� �� CornerRadius$ 20��$��0��$��0��$
��Left=�$��10, 0q�._��� $��작업 범위��$��14(�$��Center=�$��Center=��� $)��#작업 범위 입력 (행 번호): ��$��1��$��0��$
��5, 0q�$
��Left=�$��Center=��� $��1��$��1��$��60��$��Right=�$��Center=����� *
startRow+$
 PropertyChanged=��� $��부터��$��1��$��2��$��Center=�$��10, 0q��� $��2��$��1��$��60��$��Right=�$��Center=����� *endRow+$
 PropertyChanged=��� $��까지��$��2��$��2��$��Center=�$��10, 0q��� $��지번 열: ��$
��Left=�$��Center=�$
��5, 0q�$��3��$��0���� $��60��$��Center=�$��Right=�$��3��$��1������ *
addressCol+$
 PropertyChanged=��� $��공부면적:��$��4��$��0��$
��5, 0q�$
��Left=�$��Center=��� $��60��$��4��$��1��$��Center=�$��Right=����� *	sizeCol+$
 PropertyChanged=��� $��
소유자:��$��5��$��0��$��Center=�$
��Left=�$
��5, 0q��� $��60��$��5��$��1��$��Center=�$��Right=����� *
ownerCol+$
 PropertyChanged=��� $��주소:��$��6��$��0��$��Center=�$
��Left=�$
��5, 0q��� $	��200��$��6��$��1��$��Center=�$��Right=����� *mainAddress+$
 PropertyChanged=��� $��2��$ 
Horizontal=�$
��Left=�$��Center=�$��10q�.+��� $��저장 경로��$	��100��$��40��$��14(�$��0q�
����������� *setSavePath+ ��%:   '��       �� " ��.���  �@��CornerRadius$ 20����  . $	��550��$	 WrapWithOverflow=�$��10, 0q�$��15(�$��Center=����� *
savePath+$
 PropertyChanged=��� $��발급 진행��$y�	Collapsed=�.���   �� $��55 ��� $��* � �� $��* ��� $	��150 ��� $��80 �.+��� $��0��$��0��$ 
Horizontal=�.+��� $	��100��$��40��
�������$ 20��$��0��$��0��$
��Left=�$��10, 0q�._��� $��	아이디��$��14(�$��Center=�$��Center=��� $	��150��$��15(�$��Center=����� *userId+$
 PropertyChanged=��� $	��100��$��40��
�������$ 20��$��0��$��0��$
��Left=�$��10, 0q�._��� $��비밀번호��$��14(�$��Center=�$��Center=��� $	��150��$��15(�$��Center=����� *userPw+$
 PropertyChanged=�� -    �Name$ progress��$	��150��$��20��$��Center=�$��0��$��1�� � Maximum �� *
maxValue+$
 PropertyChanged=� � Value �� *totalProgress+$
 PropertyChanged=��� -    ��Name$ 	runButton��$��시작��$��2��$��0��$��60��$��40��$��14(�
�������$��0q����� *
runProcess+���� $ 	runButton�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 20���� $��1��$��3������ *processList+$
 PropertyChanged=����� .R��� $ 
Horizontal=�.+��� $��	주소 : ���� ���� *	address+�� $%��        사진 캡쳐 여부 : ���� ���� *isCaptureDone+���� " �� �� Triggers ��  �� Binding �� *isCaptureDone+ �� Value	 false.a���  � ��
 �  ���� $(��"        데이터 수집 여부 : ���� ���� *isCrawlDone+���� " �� ��  �� *isCrawlDone+	 false.a���  � ��
 �  ��