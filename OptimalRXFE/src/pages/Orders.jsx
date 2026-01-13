import { useEffect, useState } from "react";
import api from "../api/axios";

function Orders() {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [date, setDate] = useState(new Date().toISOString().split("T")[0]); // default today

  useEffect(() => {
    const fetchOrders = async () => {
      setLoading(true);
      setError("");
      try {
        const res = await api.get(`/orders/by-date?date=${date}`);
        setOrders(res.data);
      } catch (err) {
        console.error(err);
        setError("Failed to fetch orders.");
      } finally {
        setLoading(false);
      }
    };

    fetchOrders();
  }, [date]);

  return (
    <div className="p-6 bg-gray-100 min-h-screen">
      <h1 className="text-2xl font-bold mb-4">Orders</h1>

      <div className="mb-4">
        <label className="mr-2 font-semibold">Select Date:</label>
        <input
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
          className="border px-2 py-1 rounded"
        />
      </div>

      {loading ? (
        <p>Loading...</p>
      ) : error ? (
        <p className="text-red-500">{error}</p>
      ) : (
        <div className="overflow-x-auto">
            <table className="min-w-full bg-white rounded-lg shadow text-sm">
            <thead>
                <tr className="bg-blue-600 text-white text-left">
                <th className="px-3 py-2">GOrderNo</th>
                <th className="px-3 py-2">PartyCode</th>
                <th className="px-3 py-2">Order No</th>
                <th className="px-3 py-2">L Order No</th>
                <th className="px-3 py-2">Order Date</th>
                <th className="px-3 py-2">Received On</th>
                <th className="px-3 py-2">Order Entry Time</th>
                <th className="px-3 py-2">Lens Type</th>
                <th className="px-3 py-2">Coat Color</th>
                <th className="px-3 py-2">Fitting</th>
                <th className="px-3 py-2">Tint Color</th>
                <th className="px-3 py-2">Current Stage</th>
                <th className="px-3 py-2">Register No</th>
                <th className="px-3 py-2">Party Cust Code</th>
                <th className="px-3 py-2">Location Code</th>
                <th className="px-3 py-2">Stock Order</th>
                <th className="px-3 py-2">Rate</th>
                <th className="px-3 py-2">Actual Rate</th>
                </tr>
            </thead>

            <tbody>
                {orders.map((order) => (
                <tr
                    key={order.gOrderNo}
                    className="border-b hover:bg-gray-100"
                >
                    <td className="px-3 py-2">{order.gOrderNo}</td>
                    <td className="px-3 py-2">{order.party_code}</td>
                    <td className="px-3 py-2">{order.order_No}</td>
                    <td className="px-3 py-2">{order.l_OrderNo}</td>
                    <td className="px-3 py-2">
                    {new Date(order.order_Date).toLocaleDateString()}
                    </td>
                    <td className="px-3 py-2">
                    {new Date(order.receivedOnDate).toLocaleString()}
                    </td>
                    <td className="px-3 py-2">
                    {new Date(order.orderEntryTime).toLocaleTimeString()}
                    </td>
                    <td className="px-3 py-2">{order.lens_type}</td>
                    <td className="px-3 py-2">{order.coatColor}</td>
                    <td className="px-3 py-2">{order.fitting}</td>
                    <td className="px-3 py-2">{order.tintColor || "-"}</td>
                    <td className="px-3 py-2">{order.currentStage}</td>
                    <td className="px-3 py-2">{order.registerno}</td>
                    <td className="px-3 py-2">{order.party_cust_code || "-"}</td>
                    <td className="px-3 py-2">{order.loc_code}</td>
                    <td className="px-3 py-2">{order.stockorder || "-"}</td>
                    <td className="px-3 py-2">{order.rate}</td>
                    <td className="px-3 py-2">{order.actualRate}</td>
                </tr>
                ))}
            </tbody>
            </table>

        </div>
      )}
    </div>
  );
}

export default Orders;
