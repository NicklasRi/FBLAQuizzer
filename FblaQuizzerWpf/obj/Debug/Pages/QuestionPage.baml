   M S B A M L   `   `   `  ����I  EFblaQuizzerWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FblaQuizzerWpf.PagesFblaQuizzerWpf.Pages  5      E'clr-namespace:FblaQuizzerWpf.ConvertersFblaQuizzerWpf.Converters  5      A%clr-namespace:FblaQuizzerWpf.ControlsFblaQuizzerWpf.Controls  5      N JFblaQuizzerBusiness, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulldEclr-namespace:FblaQuizzerBusiness.Models;assembly=FblaQuizzerBusinessFblaQuizzerBusiness.Models 5	      '    !FblaQuizzerWpf.Pages.QuestionPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:FblaQuizzerWpf.Pages     5      >
converters'clr-namespace:FblaQuizzerWpf.Converters     5      :controls%clr-namespace:FblaQuizzerWpf.Controls     5      ZbusinessEclr-namespace:FblaQuizzerBusiness.Models;assembly=FblaQuizzerBusiness     5	        N� Title$  QuestionPage��5       � 	Resources 5      6   0FblaQuizzerWpf.Converters.InvertBooleanConverter   boolConvert9   3FblaQuizzerWpf.Converters.BooleanToVisibleConverter  boolToVisibleConvert<  6FblaQuizzerWpf.Converters.InvertBoolToVisibleConverter  invertBoolToVisibleConvert��%W   &	        &	      &	 &       5   
   6-     5   
   6/     5   
   62   5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      6   �� 5      $
��Auto �6   6,   .+�5   
   �� -   5         ��Name$ NumberLabel��5      $��4q�6   $��20(�64   $
��Bold"�6B   $
��Wrap=�6T   6j   �� 5   
   -   5        $ 
TopicLabel��5      $��4q�6   $��1��63   $��16(�6@   $
��Wrap=�6N   6d   �� 5   
   -   5        $ 	TextLabel��5      $��4q�6   $��2��63   $
��Wrap=�6@   6V   �� 5   
   $��3��6   ��6(   ��  5        MultipleChoiceTemplate;   5FblaQuizzerWpf.Controls.MultipleChoiceQuestionControl-   'FblaQuizzerWpf.Controls.QuestionControl   QuizQuestion   Options  TrueFalseTemplate8   2FblaQuizzerWpf.Controls.TrueOrFalseQuestionControl  TextTemplate1   +FblaQuizzerWpf.Controls.TextQuestionControl  MatchingTemplate5   /FblaQuizzerWpf.Controls.MatchingQuestionControl   MatchingAnswers��%�  &	       &	 t     &	 �     &	     �� 5       .R�5!         6=   �� *QuizQuestion+ 6c   �� *MultipleChoiceOptions+6�   5"      �� 5#      .R�5$         6:   �� *QuizQuestion+6b   5%      �� 5&      .R�5'         63   �� *QuizQuestion+6[   5(      �� 5)      .R�5*         67   �� *QuizQuestion+ 6]   �� *MatchingAnswers+6�   5+      5,      ��5-      �� 5.       �� 
TargetType" ��6    �� Triggers 5/      �� 50      	 �� Binding	 6&   �� *IsMultipleChoiceQuestion+
 �� Value
 True6S   .a�51      ��  �� Property � �� �� Value# [ 6@   6r   52      �� 53      	 6&   �� *IsTrueFalseQuestion+
 True6N   .a�54      ��  � ��# [ 6@   6m   55      �� 56      	 6&   �� *IsTextQuestion+
 True6I   .a�57      ��  � ��# [ 6@   6h   58      �� 59      	 6&   �� *IsMatchingQuestion+
 True6M   .a�5:      ��  � ��# [ 6@   6l   5;      5=      5?      � 5@   
   $��4��6    � ColumnDefinitions 5A      �� 5B      6%   �� 5C      6%   .+�5E      �� -   5         ��Name$ PreviousButton��5E      $��Previous��6-   $	��100��6@   $
��Left=�6L   $��10q�6g   {�6�   �� *IsFirstQuestion+ �� 	Converter# [  6�   �� 5F      -   5        $ 
NextButton��5F      $
��Next��6)   $	��100��68   $��Right=�6D   $��1��6`   $��10q�6p   y�6�   �� *IsLastQuestion+# [ 6�   �� 5G      -   5        $ FinishButton��5G      $��Finish��6+   $	��100��6<   $��Right=�6H   $��1��6d   $��10q�6t   y�6�   �� *IsLastQuestion+# [ 6�   5H      5I      5J      