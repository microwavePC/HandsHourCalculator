using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Services;
using HandsHourCalculator.Models;

namespace HandsHourCalculator.ViewModels
{
    /// <summary>
    /// MainPage.xamlに対応するViewModelクラス。
    /// MVVM用のフレームワーク「Prism.Forms」を使っているため、
    /// 開発者が意識的に設定を書かなくても、View（MainPage.xaml）とViewModel（MainPageViewModel.cs）が自動で紐付く。
    /// </summary>
    public class MainPageViewModel : BindableBase, INavigationAware
    {

        #region CONSTANTS

        // 計算内容のインデックスに対応する定数
        private const int INDEX_DOUBLE = 0;
        private const int INDEX_TEN_TIMES = 1;
        private const int INDEX_ONE_HUNDRED_TIMES = 2;

        // 画像リソースのファイル名
        private const string IMAGE_BASE = "hanzawa.png";
        private const string IMAGE_DOUBLE = "baigaeshi.png";
        private const string IMAGE_TEN_TIMES = "baigaeshi_10.png";
        private const string IMAGE_ONE_HUNDRED_TIMES = "baigaeshi_100.png";

        // ダイアログ用の文言
        private const string STR_DLG_TITLE_ERROR = "エラー";
        private const string STR_DLG_BTN_OK = "OK";

        #endregion


        #region PROPERTIES

        /// <summary>
        /// ユーザーが入力した文字列
        /// </summary>
        private string _inputText;

        /// <summary>
        /// ユーザーが選択した計算内容のインデックス
        /// </summary>
        private int _calcMethodIndex = -1;

        /// <summary>
        /// 画像リソースのパス
        /// </summary>
        private string _imageSourcePath = IMAGE_BASE;
        public string ImageSourcePath
		{
			get { return _imageSourcePath; }
			set { SetProperty(ref _imageSourcePath, value); }
        }

        /// <summary>
        /// 計算結果
        /// </summary>
        private double _calculationResult;

        #endregion


        #region FIELDS

        // ダイアログ表示処理を扱うサービス
        private readonly IPageDialogService _pageDialogService;

        #endregion


        #region COMMANDS

        // 計算ボタンのコマンド
        public ICommand CalculateCommand { get; }

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainPageViewModel(IPageDialogService pageDialogService)
        {
            // ダイアログ表示処理を扱うサービスの実体をフィールドに保持する。
            _pageDialogService = pageDialogService;
        }

        #endregion


        #region PUBLIC METHODS

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
			// 画面の退場完了時の処理。
			// このViewModelに対応する画面から別画面へ遷移直後、ここの処理が実行される。
			// [参考]http://www.nuits.jp/entry/prism-navigation-event-info-for-navigation-page-01#INavigationAwareOnNavigatedFrom
		}

        public void OnNavigatingTo(NavigationParameters parameters)
        {
			// 画面の入場時の処理
			// このViewModelに対応する画面へ入る直前、ここの処理が実行される。
			// [参考]http://www.nuits.jp/entry/prism-navigation-event-info-for-navigation-page-01#INavigationAwareOnNavigatingTo
		}

        public void OnNavigatedTo(NavigationParameters parameters)
        {
			// 画面の入場完了時の処理。
			// このViewModelに対応する画面が呼び出された直後、ここの処理が実行される。
			// [参考]http://www.nuits.jp/entry/prism-navigation-event-info-for-navigation-page-01#INavigationAwareOnNavigatedTo
		}

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// 計算処理コマンドの実処理
        /// </summary>
        private void calculate()
        {
            // 入力値が空欄の場合、エラーダイアログを出し処理を中断する。
            if (string.IsNullOrEmpty(_inputText))
			{
				_pageDialogService.DisplayAlertAsync(
					title: STR_DLG_TITLE_ERROR,
					message: "数値が入力されていません。",
                    cancelButton: STR_DLG_BTN_OK
				);

				return;
            }

			double targetNum;

            // 入力値が数値でない場合、エラーダイアログを出し処理を中断する。
            if (!double.TryParse(_inputText, out targetNum))
            {
                _pageDialogService.DisplayAlertAsync(
                    title: STR_DLG_TITLE_ERROR,
                    message: "入力値が数値として正しくありません。",
                    cancelButton: STR_DLG_BTN_OK
                );

                return;
            }

			// 計算内容が選択されていない場合、エラーダイアログを出し処理を中断する。
            if (_calcMethodIndex == -1)
			{
				_pageDialogService.DisplayAlertAsync(
					title: STR_DLG_TITLE_ERROR,
					message: "計算内容が選択されていません。",
					cancelButton: STR_DLG_BTN_OK
				);

				return;
			}

            CalculateService calculator = new CalculateService();

            // 選択された計算内容に応じた計算を行い、表示画像を更新する。
            switch (_calcMethodIndex)
            {
                // Viewとバインドされているプロパティ（ここではCalculationResultとImageSourcePath）を更新すると、
                // その変更がViewに反映される。

                case INDEX_DOUBLE:
                    ImageSourcePath = IMAGE_DOUBLE;
                    break;
                case INDEX_TEN_TIMES:
                    ImageSourcePath = IMAGE_TEN_TIMES;
                    break;
                case INDEX_ONE_HUNDRED_TIMES:
                    ImageSourcePath = IMAGE_ONE_HUNDRED_TIMES;
                    break;
                default:
                    return;
            }
        }

        #endregion

    }
}
