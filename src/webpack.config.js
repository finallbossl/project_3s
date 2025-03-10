module.exports = {
    module: {
      rules: [
        {
          test: /\.js$/,
          enforce: 'pre',
          loader: 'source-map-loader',
          exclude: [
            /node_modules\/@antv\/util/,
            /node_modules\/@antv\/scale/
          ],
        },
      ],
    },
  };
  