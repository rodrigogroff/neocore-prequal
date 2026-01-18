const path = require('path');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const Dotenv = require('dotenv-webpack');
const ObfuscatorPlugin = require('webpack-obfuscator');
const TerserPlugin = require('terser-webpack-plugin');
const ESLintPlugin = require('eslint-webpack-plugin');

module.exports = (env) => {
  return {
    mode: 'production',
    target: 'web',
    devtool: 'source-map',
    context: path.resolve(__dirname, './'),
    resolve: {
      alias: {
        '@app': path.resolve(__dirname, 'src/js')
      }
    },
    entry: {
      '/src//Login': './src//js/pages/_Auth/Intranet/router.js',
      '/src//Main': './src//js/pages/Misc/Main/router.js',      
      '/src//PrequalBlacklistListing': './src//js/pages/Prequal/BlackList_Listing/router.js',      
      '/src//PrequalRestricaoListing': './src//js/pages/Prequal/Restricao/router.js', 
      '/src//PrequalSeveridadeListing': './src//js/pages/Prequal/Severidade/router.js', 
      '/src//UsuariosListing': './src//js/pages/Usuarios/Listing/router.js',
    },
    output: {
      globalObject: "this",
      path: path.resolve(__dirname, 'dist'),
    },
    optimization: {
      minimize: true,
      minimizer: [
          new TerserPlugin({
              terserOptions: {
                  format: {
                      beautify: false,
                      comments: false, 
                  },
                  compress: {
                      reduce_funcs: true,
                      inline: 2,         
                  },
                  mangle: true,         
              },
              extractComments: false,
          }),
      ],
    },
    module: {
      rules: [
        {
          test: /\.html$/,
          use: [
            {
              loader: 'html-loader',
              options: {
                minimize: true,
                sources: {
                  urlFilter: (attribute, value) => {
                    if (value && /\.(webp|png|jpe?g|gif|svg)$/i.test(value)) {
                      return false;
                    }
                    return true;
                  },
                },
              },
            },
          ],
        },
        {
          test: /\.(webp|png|jpe?g|gif|svg)$/i,
          use: 'ignore-loader',
        },
      ],
    },
    plugins: [
      new CleanWebpackPlugin(), 
      new ESLintPlugin({
        extensions: ['js'],
        failOnError: false, // Set to true if you want builds to fail on errors
        emitWarning: true,
      }),
      new Dotenv({
        path: (() => {
          if (process.env.npm_lifecycle_event === 'build') {
            return '.env.dev';
          } else if (process.env.npm_lifecycle_event === 'build_prod') {
            return '.env.prod';
          }
        })()
      }),
      ...(process.env.npm_lifecycle_event === 'build_prod' ? [new ObfuscatorPlugin()] : []),
    ],
  };
};
