import { useNavigate } from "react-router-dom";

function Dashboard() {
  const navigate = useNavigate();

  const goToOrders = () => {
    navigate("/orders");
  };

  return (
    <div className="min-h-screen p-6 bg-gray-100">
      <h1 className="text-3xl font-bold mb-6">Dashboard</h1>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {/* Orders Button */}
        <button
          onClick={goToOrders}
          className="bg-blue-600 text-white px-6 py-3 rounded-lg shadow hover:bg-blue-700 transition-colors font-semibold"
        >
          Orders
        </button>

        {/* Add more buttons for other modules */}
        <button className="bg-green-600 text-white px-6 py-3 rounded-lg shadow hover:bg-green-700 transition-colors font-semibold">
          Reports
        </button>

        <button className="bg-yellow-600 text-white px-6 py-3 rounded-lg shadow hover:bg-yellow-700 transition-colors font-semibold">
          Stock
        </button>
      </div>
    </div>
  );
}

export default Dashboard;
