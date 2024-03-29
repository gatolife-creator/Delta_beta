﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace Delta_beta.Handler
{
    class LocalFileSchemeHandlerFactory : ISchemeHandlerFactory
    {
        private static readonly String SCHEME_NAME = "localfile";

        IResourceHandler ISchemeHandlerFactory.Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            // スキーマ名がLocalFileの場合
            if (schemeName == SCHEME_NAME)
            {
                // urlでc:\からのファイルパスが指定されているとして、絶対パスを作成する。
                String path = "c:\\" + request.Url.Replace($"{SCHEME_NAME}://", "").Replace("/", "\\");

                // 指定されたファイルがあれば、その内容を返却する。
                if (System.IO.File.Exists(path))
                {
                    // 指定されたファイルの中身を返却する
                    return ResourceHandler.FromFilePath(path, "text/plain");
                }
                else
                {
                    // エラーメッセージを返却する
                    return ResourceHandler.FromString("Such file dose NOT exist.");
                }
            }

            return null;
        }
    }
}
