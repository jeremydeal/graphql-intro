// see: https://codesandbox.io/s/48p1r2roz4
import {
  ApolloServer
  //gql
} from "apollo-server";
import fetch from "node-fetch";
import _ from "lodash";

// Construct a schema, using GraphQL schema language
const typeDefs = `
  type Query {
    rates(currency: String!): [ExchangeRate]
  }

	type ExchangeRate {
		currency: String
		rate: String
		name: String
	}
`;

interface ICurrencyQuery {
  currency: string;
}

// Provide resolver functions for your schema fields
const resolvers = {
  Query: {
    rates: async (/* root: any, */ { currency }: ICurrencyQuery) => {
      try {
        const results = await fetch(
          `https://api.coinbase.com/v2/exchange-rates?currency=${currency}`
        );
        const exchangeRates = await results.json();

        console.log(exchangeRates);

        return _.map(exchangeRates.data.rates, (rate, currency) => ({
          currency,
          rate
        }));
      } catch (e) {
        console.error(e);
      }

      return null;
    }
  },
  ExchangeRate: {
    name: async ({ currency }: ICurrencyQuery) => {
      try {
        const results = await fetch("https://api.coinbase.com/v2/currencies");
        const currencyData = await results.json();

        const currencyInfo = currencyData.data.find(
          (c: any) => c.id.toUpperCase() === currency
        );
        return currencyInfo ? currencyInfo.name : null;
      } catch (e) {
        console.error(e);
      }

      return null;
    }
  }
};

const server = new ApolloServer({
  typeDefs,
  resolvers
});

server.listen().then(({ url }) => {
  console.log(`🚀 Server ready at ${url}`);
});
