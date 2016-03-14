/**********************************************************\
|                                                          |
|                          hprose                          |
|                                                          |
| Official WebSite: http://www.hprose.com/                 |
|                   http://www.hprose.org/                 |
|                                                          |
\**********************************************************/
/**********************************************************\
 *                                                        *
 * HproseCallback.cs                                      *
 *                                                        *
 * hprose callback class for C#.                          *
 *                                                        *
 * LastModified: Jan 18, 2016                             *
 * Author: Ma Bingyao <andot@hprose.com>                  *
 *                                                        *
\**********************************************************/
using System;
namespace Hprose.Common {
#if !(dotNET10 || dotNET11 || dotNETCF10 || dotNETMF)
    public delegate void HproseCallback<T>(T result, object[] args);
    public delegate void HproseCallback1<T>(T result);
#endif
    public delegate void HproseCallback(object result, object[] args);
    public delegate void HproseCallback1(object result);
    public delegate void HproseErrorEvent(string name, Exception e);
}