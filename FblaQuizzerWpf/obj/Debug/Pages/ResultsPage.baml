   M S B A M L   `   `   `  ����I  EFblaQuizzerWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FblaQuizzerWpf.PagesFblaQuizzerWpf.Pages  5      E'clr-namespace:FblaQuizzerWpf.ConvertersFblaQuizzerWpf.Converters  5      N JFblaQuizzerBusiness, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulldEclr-namespace:FblaQuizzerBusiness.Models;assembly=FblaQuizzerBusinessFblaQuizzerBusiness.Models 5      &     FblaQuizzerWpf.Pages.ResultsPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:FblaQuizzerWpf.Pages     5      >
converters'clr-namespace:FblaQuizzerWpf.Converters     5      XmodelsEclr-namespace:FblaQuizzerBusiness.Models;assembly=FblaQuizzerBusiness     5        N� Title$  ResultsPage��5       � 	Resources 5      >  8FblaQuizzerWpf.Converters.QuestionTypeToVisibleConverter "  questionTypeToVisibleConverter6   0FblaQuizzerWpf.Converters.BoolToCorrectConverter  boolToCorrectConvert4   .FblaQuizzerWpf.Converters.BoolToColorConverter  boolToColorConvert��%W   &	        &	      &	 &       5   
   6]     5   
   6K     5   
   6G   5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      6   �� 5      $
��Auto �6   6,   .+�5   
   �� $
��Auto=�6   .��5      �  5      �� 5      6&   �� 5      6&   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Your score:��6    $��14(�63   $
��Bold"�6A   $��4q�6S   6`   �� 5       $��4q�6    $��14(�6d   $
��Bold"�6r   ��6+   �� *
Quiz.Score+ �� StringFormat$ {0:#00.0'%'}��6�   5!      �� 5#      $��1��6?   ��6   �� *	Results+��5$      �� 5%      .R�5&      � $��10q�6#    5'   "   �� 5(   &   66   �� 5)   &   66   �� 5*   &   66   �� 5+   &   66   �� 5,   &   66   .+�5.   "   �� $��16(�6L   $
��Bold"�6Z   ��6,   �� *QuestionNumber+6n   �� 5/   "   $��1��6,   $��14(�6P   $
��Bold"�6^   ��69   �� *Topic+6r   �� 50   "   $��2��6,   ��69   �� *Text+6Q   �� 51   "   $��3��6,   $
��Bold"�6�   ��69   �� *	Correct+ �� 	Converter# [ ��6�   �� *	Correct+# [ 6�   � 52   "   $��4��6'   y�64   �� *QuestionType+# [  -  'FblaQuizzerBusiness.Models.QuestionType   MultipleChoice �� ConverterParameter# Z  53   &   �� 54   *   6:   �� 55   *   6:   .+�57   &   �� ��63   �� *	Options+��58   *   �� 59   .   .R�5:   2   �  � ColumnDefinitions 5;   6   �� 5<   :   $
��Auto �6K   6Z   �� 5=   :   6M   .+�5?   6   �� $��4q�6@   ��6K   �� *Letter+6e   �� 5@   6   $��4q�6@   $��1��6K   ��6[   �� *Text+6s   5A   3   5B   /   5D   '   �� 5E   &   $��1��61   $ Vertical=�6>   .+�5F   *   �� $��Your answer:��64   6J   �� 5G   *   ��64   �� *UserAnswer.Letter+6Y   �� 5H   *   $��Key answer:��64   6I   �� 5I   *   ��64   �� *KeyAnswer.Letter+6X   5J   '   5K   #   � 5L   "   $��4��6'   y�64   �� *QuestionType+# [  	   	TrueFalse# Z	 .+�5M   &   �� $ Vertical=�61   .+�5N   *   �� $��Your answer:��64   6J   �� 5O   *   ��64   �� *
UserAnswer+6R   �� 5P   *   $��Key answer:��64   6I   �� 5Q   *   ��64   �� *	KeyAnswer+6Q   5R   '   5S   #   � 5T   "   $��4��6'   y�64   �� *QuestionType+# [  
   Text# Z
 .+�5U   &   �� $ Vertical=�61   .+�5V   *   �� $��Your answer:��64   6J   �� 5W   *   ��64   �� *
UserAnswer+6R   �� 5X   *   $��Key answer:��64   6I   �� 5Y   *   ��64   �� *	KeyAnswer+6Q   5Z   '   5[   #   � 5\   "   $��4��6'   y�64   �� *QuestionType+# [     Matching# Z  5]   &   �� 5^   *   6:   �� 5_   *   6:   .+�5a   &   �  5b   *   �� 5c   .   6A   �� 5d   .   6A   �� 5e   .   6A   .+�5g   *   �� $��Prompt��64   $
��Bold"�6B   6V   �� 5h   *   $��1��64   $��Your answer��6D   $
��Bold"�6W   6k   �� 5i   *   $��2��64   $��
Key answer��6D   $
��Bold"�6V   6j   5j   '   �� 5k   &   $��1��6[   ��63   �� *MatchingResults+��5l   *   �� 5m   .   .R�5n   2   �  5o   6   �� 5p   :   6M   �� 5q   :   6M   �� 5r   :   6M   .+�5t   6   �� $
��Wrap=�6@   ��6T   �� *Prompt+6n   �� 5u   6   $
��Wrap=�6@   $��1��6T   ��6d   �� *UserAnswerOption+6�   �� 5v   6   $
��Wrap=�6@   $��2��6T   ��6d   �� *KeyAnswerOption+6�   5w   3   5x   /   5z   '   5{   #   5|      5}      5      5�      5�      � 5�   
   $��1��6    5�      �� 5�      6%   �� 5�      6%   .+�5�      �� -   5         ��Name$ PrintButton��5�   H   $
��Left=�6   $	��100��60   $��10q�6<   $��Print��6]   6�   �� 5�      -   5        $ CloseButton��5�   =   $��1��6   $	��100��6%   $��10q�61   $��Close��6R   $��Right=�6b   6�   5�      5�      5�      