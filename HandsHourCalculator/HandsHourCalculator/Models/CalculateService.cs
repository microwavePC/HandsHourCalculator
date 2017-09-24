using System;
namespace HandsHourCalculator.Models
{
    /// <summary>
    /// 計算処理のビジネスロジックを扱うクラス
    /// </summary>
    public class CalculateService
    {
        
        #region PUBLIC METHODS

        /// <summary>
        /// 引数で渡された数値を倍返しする処理
        /// </summary>
        /// <returns>引数で渡された数値を2倍にした値</returns>
        /// <param name="targetNum">倍返ししたい数値</param>
        public double ToDoubleNumber(double targetNum)
        {
            return targetNum * 2;
        }

        /// <summary>
        /// 引数で渡された数値を10倍返しする処理
        /// </summary>
        /// <returns>引数で渡された数値を10倍にした値</returns>
        /// <param name="targetNum">10倍返ししたい数値</param>
		public double ToTenTimes(double targetNum)
        {
            return targetNum * 10;
        }

        /// <summary>
        /// 引数で渡された数値を100倍返しする処理
        /// </summary>
        /// <returns>引数で渡された数値を100倍にした値</returns>
        /// <param name="targetNum">100倍返ししたい数値</param>
        public double ToOneHundredTimes(double targetNum)
        {
            return targetNum * 100;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
