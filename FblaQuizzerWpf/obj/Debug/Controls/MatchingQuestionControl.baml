   M S B A M L   `   `   `  ����I  EFblaQuizzerWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:FblaQuizzerWpf.ControlsFblaQuizzerWpf.Controls  5      5    /FblaQuizzerWpf.Controls.MatchingQuestionControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:FblaQuizzerWpf.Controls     5      -   5        -   'FblaQuizzerWpf.Controls.QuestionControl   Name$
  root��5	      .��5
      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   �� -   5         ��Name$ PromptItems��5      ��6,   ��  �� Path$ MatchingAnswers� �� ElementName$
 root����5      �� 5      .R�5      �� $��10q�6!   .+�5      �� 
�������$��1q�64    �� CornerRadius$ 5��6H   ._�5      �� $
��Auto=�6+   $��40��6N   .��5      �� $��WrapWithOverflow=�6(   $��4q�6d   ��6H   �� *
PromptText+6q   5      5      5      5      5      �� 5   
   -   5        $ OptionItems��5      $��1��6k    J� 	AllowDrop . ��6,   �� $ MatchingAnswers�$
 root����5      �� 5      .R�5      �� -   5        $��10q�5   !   .+�5       �� 
�������$��1q�64   $ 5��6H   ._�5!      �� $
��Auto=�6+   $��40��6N   .��5"      �� $
��Wrap=�6(   $��4q�6   
 � Tag 6<   �� *MatchingAnswerOptionId+��6c   �� *
OptionText+6�   5#      5$      5%      5&      5(      5)      5*      